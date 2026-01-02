using Microsoft.AspNetCore.Mvc;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]            // GET api/helloworld
        public IActionResult Index()
        {
            return Ok("Hello World ..."); // or return Content("Hello World ...");
        }
    }
}
