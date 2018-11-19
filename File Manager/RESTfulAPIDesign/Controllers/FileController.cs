using Microsoft.AspNetCore.Mvc;
using RESTfulAPIDesign.Services;
using Microsoft.AspNetCore.Authorization;
using RESTfulAPIDesign.Models;
using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FileController : Controller
    {
        private IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(byte []), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult GetPDFFile()
        {   
            // This return byte array
            byte[] buffer = this.fileService.GetPDFFile();
            if (buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }
    }
}
