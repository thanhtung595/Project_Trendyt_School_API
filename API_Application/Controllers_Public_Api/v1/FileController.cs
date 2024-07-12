using API_Application.Service.FileService;
using Lib_Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_Application.Controllers_Public_Api.v1
{
    [Route("/")]
    [ApiController]
    public class FileController : ControllerBase
    {
        //[HttpGet("baitap/{router}"), Authorize] // ,Authorize
        //public IActionResult Get([FromRoute] string router)
        //{
        //    var (fileBytes, mimeType) = FileInFolder.ReadFile(router, StringFilePath.File_BaiTap);
        //    if (fileBytes != null)
        //    {
        //        return File(fileBytes, mimeType);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
