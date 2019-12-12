using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PopCorn.Api.Common.Results
{

    /// <summary>
    /// Returns a status 500 (Internal Server Error) result with no message
    /// </summary>
    public partial class InternalServerErrorResult : StatusCodeResult
    {

        public InternalServerErrorResult() : base(StatusCodes.Status500InternalServerError)
        {

        }

    }
}
