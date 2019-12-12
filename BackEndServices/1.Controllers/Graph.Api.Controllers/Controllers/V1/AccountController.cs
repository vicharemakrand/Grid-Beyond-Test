using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
 using Graph.Utility;
using Graph.ViewModels;
using Graph.ViewModels.Paging;
using System;
using System.Net;
using System.Threading.Tasks;
using Graph.ViewModels;
using PopCorn.Api.Common.Extensions;
using PopCorn.Api.Common.HaoasLinks;
using Graph.IDomainServices.Identity;

namespace PopCorn.Api.Auth.Controllers.Controllers.V1
{

    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class AccountController : BaseController
    {
        private UserManager<IdentityUserViewModel> userManager;
        private SignInManager<IdentityUserViewModel> signInManager;
        private readonly IUserService userService;

        public AccountController(UserManager<IdentityUserViewModel> _userManager,
            SignInManager<IdentityUserViewModel> _signInManager,
            IUserService _userService,
            IHaoasLinkProvider _haoasLinks) : base(_haoasLinks)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
            this.userService = _userService;

        }

        // POST api/Account/Register
        [Route("Register"), AllowAnonymous]
        [HttpPost]
        public async virtual Task<IActionResult> Register([FromBody]RegisterBindingModel model)
        {
            model.Password = AppConstants.DefaultPassword;
            var user = new IdentityUserViewModel() {
                            UserName = AppMethods.GenerateUserName(model.Email) + DateTime.Now.Ticks,
                            Email = model.Email
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            }

            IActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok(result.Succeeded);
        }

        [Route("GetUsers")]
        [HttpGet]
        public virtual IActionResult GetUsers(PagingParams paging)
        {
            try
            {
                var lookupList = AddLinks(userService.PageAll(paging));
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(ex);
            }
        }

        private IActionResult GetErrorResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        [Route("Save"), AllowAnonymous]
        [HttpPost]
        public async virtual Task<IActionResult> Save([FromBody]IdentityUserViewModel user)
        {
            IdentityResult result = await userManager.UpdateAsync(user);
            IActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok(result.Succeeded);
        }

        [Route("GetUser")]
        [HttpGet]
        public virtual IActionResult GetUser(int userId)
        {
            try
            {
                var lookupList = AddLinks(userService.GetById(userId));
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(ex);
            }
        }
    }
}
