using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCar.Controllers
{
    public class MessagingController : Controller
    {
        private DalMessagingManagement dalFinancialManagement;
        public MessagingController()
        {
            dalFinancialManagement = new DalMessagingManagement();
        }

        public ActionResult UserReporting()
        {
            return View();

        }

        public ActionResult Message()
        {
            return View();

        }

        public IActionResult AdminInbox()
        {
            return View();
        }
    }
}
