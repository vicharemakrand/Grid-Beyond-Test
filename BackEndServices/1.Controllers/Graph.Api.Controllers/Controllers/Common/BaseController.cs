using Microsoft.AspNetCore.Mvc;
using Graph.ServiceResponse;
using Graph.ViewModels.Core;
using PopCorn.Api.Common.HaoasLinks;

namespace PopCorn.Api.Auth.Controllers.Controllers
{
    //[Produces("application/json")]
    //[Route("api/[controller]")]
    public abstract partial class BaseController : ControllerBase
    {
        public readonly IHaoasLinkProvider haoasLinks;


        public BaseController(IHaoasLinkProvider _haoasLinks)
        {
            this.haoasLinks = _haoasLinks;
        }

        public HaoasLinkWrapper<VM> AddLinks<VM>(ResponseResult<VM> responseResult, string linkName = null) where VM : BaseViewModel
        {
            var wrapper = new HaoasLinkWrapper<VM>() { Links = haoasLinks.GetLinks(responseResult.ViewModel, linkName), ResponseResult = responseResult };
            return wrapper;
        }

        public HaoasLinkWrappers<VM> AddLinks<VM>(ResponseResults<VM> responseResults, string linkName = null) where VM : BaseViewModel
        {
            var wrapper = new HaoasLinkWrappers<VM>() { Links = haoasLinks.GetListLinks(responseResults.ViewModels, linkName), ResponseResults = responseResults };
            return wrapper;
        }

        public HaoasLinkPagingWrappers<VM> AddLinks<VM>(ResponsePagedResults<VM> responseResults, string linkName = null) where VM : BaseViewModel
        {
            var wrapper = new HaoasLinkPagingWrappers<VM>() { Links = haoasLinks.GetPagedLinks(responseResults.ViewModels, linkName), ResponseResults = responseResults };
            return wrapper;
        }
    }
}