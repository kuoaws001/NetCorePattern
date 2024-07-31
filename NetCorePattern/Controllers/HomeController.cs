using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using NetCorePattern.Models;
using NetCorePattern.Service;
using NetCorePattern.Utils;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NetCorePattern.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IFileService fileService;
        private readonly IConfiguration configuration;

        public HomeController(IFileService fileService, IConfiguration configuration)
        {
            this.fileService = fileService;
            this.configuration = configuration;
        }

        [HttpGet]
        [Route("/api/token")]
        public IActionResult GetToken()
        {
            return Ok(new
            {
                token = "orange"
            });
        }

        [HttpPost]
        [Route("/api/addProduct")]

        public IActionResult AddProduct([FromForm] Product product)
        {
            var result = fileService.SaveImage(product.ImageFile!);
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

        [HttpPost]
        [Route("/api/webhook")]

        public async Task<IActionResult> TestWebHook([FromForm] WebHookForm form)
        {
            string url = $"https://www.surveycake.com/webhook/v0/{form.Svid}/{form.Hash}";

            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync(url);
            string cipher = await response.Content.ReadAsStringAsync();

            //string cipher = "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";
            string hashkey = configuration.GetSection("WebHook:HashKey").Value;
            string ivKey = configuration.GetSection("WebHook:IVkey").Value;

            //string result = Crypto.Decrypt(cipher, hashkey, ivKey);
            AESHelper helper = new AESHelper(hashkey, ivKey);
            var jsonStr = helper.decrypt(cipher);
            string strResult = Regex.Replace(jsonStr, "\u000f", string.Empty);

            var result = JsonConvert.DeserializeObject<WebHookResponse>(strResult);

            return Ok(result);
        }
    }
}
