using Microsoft.AspNetCore.Mvc;

namespace WebBilling_Lahore_ReactCore.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
