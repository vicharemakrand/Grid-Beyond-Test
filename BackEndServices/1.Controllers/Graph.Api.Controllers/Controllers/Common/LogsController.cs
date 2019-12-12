using Microsoft.AspNetCore.Mvc;
using Graph.ViewModels.User;
using System.Collections.Generic;
using System.Net;
using Graph.ViewModels;
using PopCorn.Api.Common.Extensions;
using PopCorn.Api.Common.HaoasLinks;

namespace PopCorn.Api.Auth.Controllers.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class LogsController : BaseController
    {
        public LogsController(IHaoasLinkProvider _haoasLinks) : base(_haoasLinks)
        {
        }

        // GET api/values
        [HttpGet]
        public virtual IActionResult Get()
        {
            var result = new List<UserViewModel>() {
                new UserViewModel { Email ="User1" },
                new UserViewModel { Email ="User2" },
                new UserViewModel { Email ="User3" },
                new UserViewModel { Email ="User4" }
           };
            if (result.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { result, message = "found records" });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Record Found");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public virtual string Get(long id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public virtual void Post([FromBody]LogsBindingModel value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public virtual void Put(long id, [FromBody]LogsBindingModel value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public virtual void Delete(long id)
        {
        }
    }
}
