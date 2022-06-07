using Microsoft.AspNetCore.Mvc;

namespace EcoCar.Controllers
{
    public class HomeController : Controller
    {
        //Launching Page
        public IActionResult Index()
        {
            return View();
        }
        //About Page
        public IActionResult EcoCarValues()
        {
            return View();
        }
    }
}
