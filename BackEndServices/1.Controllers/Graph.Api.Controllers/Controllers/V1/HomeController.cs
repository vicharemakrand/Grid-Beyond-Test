using Microsoft.AspNetCore.Mvc;
using Graph.ViewModels;
using System;
using System.Net;
using PopCorn.Api.Common.Extensions;
using PopCorn.Api.Common.HaoasLinks;

namespace PopCorn.Api.Auth.Controllers.Controllers.V1
{


    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class HomeController : BaseController
    {
        public HomeController(IHaoasLinkProvider _haoasLinks):base(_haoasLinks)
        {
        }

        [Route("GetHaosLinks")]
        [HttpGet]
        public virtual IActionResult GetHaosLinks(string linkName = null)
        {
            try
            {
                var lookupList = AddLinks(new Graph.ServiceResponse.ResponseResult<HomeViewModel>(), linkName);
                return Request.CreateResponse(HttpStatusCode.OK, lookupList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(ex);
            }
        }

    }
}
