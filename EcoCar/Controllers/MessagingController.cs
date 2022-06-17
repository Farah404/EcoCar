using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcoCar.Controllers
{
    public class MessagingController : Controller
    {
        private DalMessagingManagement dalMessagingManagement;
        private DalFinancialManagement dalFinancialManagement;
        private DalPersonManagement dalPersonManagement;
        public MessagingController()
        {
            dalMessagingManagement = new DalMessagingManagement();
            dalPersonManagement = new DalPersonManagement();
            dalFinancialManagement = new DalFinancialManagement();
        }

        #region Contacting an Admin: help or reporting other user
        public ActionResult UserReporting(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            MessagingViewModel messagingViewModel = new MessagingViewModel();
            return View();

        }
        //[HttpPost]
        //public IActionResult UserReporting()

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