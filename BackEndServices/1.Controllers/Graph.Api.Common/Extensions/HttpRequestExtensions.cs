using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PopCorn.Api.Common.Extensions
{
    public static partial class HttpRequestExtensions
    {
        public static IActionResult CreateResponse(this HttpRequest request, HttpStatusCode status, object content)
        {
            return new ObjectResult(content)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult CreateErrorResponse(this HttpRequest request, object content)
        {
            return new ObjectResult(content)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
