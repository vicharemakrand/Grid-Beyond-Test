using Microsoft.AspNetCore.Mvc;
using System.Net;
using PopCorn.Api.Common.Extensions;
using Graph.IDomainServices;
using System.IO;
using System.Net.Http.Headers;
using ExcelDataReader;
using System.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using Graph.Utility;
using Graph.ServiceResponse;

namespace Graph.Api.Controllers.V1
{


    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public partial class UploadController : BaseController
    {
        protected readonly IGraphService graphService;
        public UploadController(IGraphService _graphService) : base()
        {
            graphService = _graphService;
        }

        [Route("UploadFile/{overWrite}")]
        [HttpPost, DisableRequestSizeLimit]
        public virtual IActionResult UploadFile(bool overWrite)
        {
            var file = Request.Form.Files[0];
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "GraphDataFiles");
            var errorMessage = AppMessages.ACTION_UPLOAD_FAILED_UNKOWN;
            var isSucceed = false;
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileInfo = new FileInfo(fileName);
                if(fileInfo.Extension == ".csv")
                {
                    using (var stream = new FileStream(Path.Combine(pathToSave, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                        using (var reader = ExcelReaderFactory.CreateCsvReader(stream,
                            new ExcelReaderConfiguration()
                            {
                                FallbackEncoding = Encoding.GetEncoding(1252),
                                AutodetectSeparators = new char[] { ',', ';', '\t', '|', '#' }
                            }))
                        {
                            var graphData = reader.AsDataSet(new ExcelDataSetConfiguration()
                            {
                                UseColumnDataType = false,
                                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                            }).Tables[0];

                            graphService.SaveAll(graphData, overWrite);
                            reader.Close();
                        }

                        stream.Close();
                    }
                    isSucceed = true;
                }
                else
                {
                    errorMessage = AppMessages.ACTION_UPLOAD_FAILED_WRONG_FORMAT;
                }

                System.IO.File.Delete(Path.Combine(pathToSave, fileName));
            }

            if (isSucceed)
            {
                return Ok(AppMessages.ACTION_UPLOAD_SUCCEEDED);
            }

            return BadRequest(errorMessage);
        }

    }
}
