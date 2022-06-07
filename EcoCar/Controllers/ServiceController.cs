using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcoCar.Controllers
{
    public class ServiceController : Controller
    {
        private DalServiceManagement dalServiceManagement;
        public ServiceController()
        {
            dalServiceManagement = new DalServiceManagement();
        }

        public ActionResult SearchService()
        {
            return View();

        }

        public ActionResult CreateService()
        {
            return View();

        }

        public ActionResult CreateCarPoolingService()
        {
            return View();

        }

        public ActionResult CreateCarRentalService()
        {
            return View();

        }

        public ActionResult CreateParcelService()
        {
            return View();

        }
      
    }
}
