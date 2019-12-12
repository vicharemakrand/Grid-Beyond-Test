using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PopCorn.Api.Common.Results
{
    public partial class ForbiddenObjectResult : ObjectResult
    {

        public ForbiddenObjectResult(object value) : base(value)
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }

    }
}
