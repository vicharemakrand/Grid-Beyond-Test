using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Graph.ServiceResponse;
using Graph.Utility;
using Graph.ViewModels;
using Graph.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PopCorn.Api.Common.HaoasLinks;

namespace PopCorn.Api.Auth.Controllers.Controllers.V1
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class AuthController : BaseController
    {
        private SignInManager<IdentityUserViewModel> signInManager;
        private UserManager<IdentityUserViewModel> userManager;
        private IConfiguration configuration;


        public AuthController(SignInManager<IdentityUserViewModel> _signInManager, 
                                UserManager<IdentityUserViewModel> _userManager,
                                IConfiguration _configuration, IHaoasLinkProvider _haoasLinks) : base(_haoasLinks)
        {
                signInManager = _signInManager;
                userManager = _userManager;
                configuration = _configuration;
         }


         [HttpPost, Route("login")]
        public async virtual Task<IActionResult> Login([FromBody]LoginViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var result = await signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var tuple = GetUserInfoAndToken(viewModel.Email).Result;
                ResponseResult<LoginTokenViewModel> responseResult =
                    new ResponseResult<LoginTokenViewModel> {
                        ViewModel = new LoginTokenViewModel { Token = tuple.Item1, UserInfo = tuple.Item2, IsAuth = true },
                        IsSucceed = true,
                        Message = AppMessages.ActionMessage_Succeed
                    };
                return Ok(AddLinks(responseResult));
            }
            else
            {
                return Unauthorized();
            }
        }

        private async Task<(string, IdentityUserViewModel)> GetUserInfoAndToken(string email)
        {
            var userRoles = await userManager.GetRolesAsync(new IdentityUserViewModel { Email = email });
            var user = await userManager.FindByEmailAsync(email);
            user.RoleName = userRoles.FirstOrDefault();

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.TokenPrivateKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, email),
                    new Claim(ClaimTypes.Role, string.Join(",",userRoles))
                };

            var tokeOptions = new JwtSecurityToken(
                issuer: configuration.GetValue<string>(AppConstants.BaseUrlKey),
                audience: configuration.GetValue<string>(AppConstants.BaseUrlKey),
                claims: claims,
                expires: DateTime.Now.AddMinutes(350),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return (tokenString, user);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async virtual Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");
            return Ok("Home");
        }

    }
}