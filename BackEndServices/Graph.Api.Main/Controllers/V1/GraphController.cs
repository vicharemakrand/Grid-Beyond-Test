using Microsoft.AspNetCore.Mvc;
using System.Net;
using PopCorn.Api.Common.Extensions;
using Graph.IDomainServices;

namespace Graph.Api.Controllers.V1
{


    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class GraphController : BaseController
    {
        protected readonly IGraphService graphService;
        public GraphController(IGraphService _graphService) :base()
        {
            graphService = _graphService;
        }

        [Route("GetData")]
        [HttpGet]
        public virtual IActionResult GetData()
        {
            var graphData = graphService.GetGraphData();
            return Request.CreateResponse(HttpStatusCode.OK, graphData);
        }

    }
}
