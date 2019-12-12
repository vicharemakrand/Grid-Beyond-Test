using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PopCorn.Api.Common.Results
{

    /// <summary>
    /// Returns a status 403 (Forbidden) result with no message
    /// </summary>
    public partial class ForbiddenResult : StatusCodeResult
    {

        public ForbiddenResult() : base(StatusCodes.Status403Forbidden)
        {

        }

    }
}
