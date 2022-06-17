using EcoCar.Models.MessagingManagement;
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




        #endregion

        #region Admin inbox and response
        public IActionResult AdminInbox()
        {
            return View();
        }
        #endregion


        

        
    }
}