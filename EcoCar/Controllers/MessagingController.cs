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

        #region Contacting an Admin: help or reporting other user
        public ActionResult UserReporting()
        {
            return View();

        }

        public IActionResult AdminInbox()
        {
            return View();
        }
        #endregion

        # region Contacting other users concerning a service they proposed 
        public ActionResult Message()
        {
            return View();

        }

        public IActionResult EcoUserInbox()
        {
            return View();
        }
        #endregion
    }
}
