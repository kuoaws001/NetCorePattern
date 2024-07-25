using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using NetCorePattern.Models;
using NetCorePattern.Service;
using System.Reflection;

namespace NetCorePattern.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFileService fileService;

        public HomeController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet]
        [Route("api/token")]
        public IActionResult GetToken()
        {
            return Ok(new
            {
                token = "orange"
            });
        }

        [HttpPost]
        [Route("api/addProduct")]

        public IActionResult AddProduct([FromForm] IFormFile file)
        {
            var result = fileService.SaveImage(file);
            if (result.Item1)
            {
                return Ok(new
                {
                    token = "orange"
                });
            }
            else
            {
                return Ok(new
                {
                    token = "tina"
                });
            }
        }
    }
}
