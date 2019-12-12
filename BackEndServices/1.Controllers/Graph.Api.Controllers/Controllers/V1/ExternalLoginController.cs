using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Graph.Utility;
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
    public partial class ExternalLoginController : BaseController
    {
        private readonly UserManager<IdentityUserViewModel> userManager;
        private readonly SignInManager<IdentityUserViewModel> signInManager;
        private IConfiguration configuration;

        public ExternalLoginController(
            UserManager<IdentityUserViewModel> _userManager,
            SignInManager<IdentityUserViewModel> _signInManager,
            IConfiguration _configuration, IHaoasLinkProvider _haoasLinks) : base(_haoasLinks)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            configuration = _configuration;
        }

        [HttpGet, Route("Login"), AllowAnonymous]
        public virtual IActionResult Login(string provider,string clientId, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
             signInManager.SignOutAsync().Wait();

            var redirectUrl = Url.Action(nameof(LoginCallback), "ExternalLogin", new { returnUrl, clientId });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet, Route("LoginCallback")]
        public async virtual Task<IActionResult> LoginCallback(string returnUrl = null, string access_token=null, string clientId = null, string remoteError = null)
        {
            var javascriptContent = new StringBuilder();
            var returnPageName = "";
            if (remoteError != null)
            {
                //ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToAction(nameof(Login));
            }
            var info = await signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            // Sign in the user with this external login provider if the user already has a login.
   
            var result = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);


            if (result.Succeeded)
            {
                if (result.IsLockedOut || result.IsNotAllowed)
                {
                    returnPageName = "lockedOut/" + info.LoginProvider + "/" + result.IsLockedOut+ "/" + result.IsNotAllowed + "/" + email;
                }

                var tuple = GetUserInfoAndToken(email).Result;

                await signInManager.SignInAsync(tuple.Item2, isPersistent: false);

                returnPageName = "stopwatch/" + true + "/" + tuple.Item2.UserName + "/" + tuple.Item2.RoleName + "/" + tuple.Item2.Id + "/" + tuple.Item1;
            }
            else
            {
                returnPageName = "registerExternal/" + info.LoginProvider + "/" + info.ProviderKey + "/" + info.ProviderDisplayName + "/" + email;
            }

            javascriptContent.Append("<script type=\"text/javascript\">");
            javascriptContent.Append("window.location=\""+ returnUrl + returnPageName + "\"");
            javascriptContent.Append("</script>");

            return Content(javascriptContent.ToString(), "text/html");
        }

        [HttpPost, Route("LoginConfirmation")]
        public async virtual Task<IActionResult> LoginConfirmation([FromBody]ExternalLoginViewModel registration)
        {
            if (ModelState.IsValid)
            {
                var info = new UserLoginInfo(registration.LoginProvider, registration.ProviderKey, registration.ProviderDisplayName);
                var user = new IdentityUserViewModel { UserName = AppMethods.GenerateUserName(registration.Email), Email = registration.Email };
                var response = await userManager.CreateAsync(user);
                if (response.Succeeded)
                {
                    response = await userManager.AddToRoleAsync(user, UserRoles.User.ToString());
                }
                if (response.Succeeded)
                {
                    user.RoleName = UserRoles.User.ToString();
                    response = await userManager.AddLoginAsync(user, info);
                    if (response.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        var tuple = GetUserInfoAndToken(registration.Email).Result;

                        //_logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return Ok(new { Token = tuple.Item1, UserInfo = tuple.Item2, IsAuth = true});
                    }
                }
                AddErrors(response);
            }

            return BadRequest(ModelState);
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

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}