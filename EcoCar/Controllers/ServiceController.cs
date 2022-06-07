using EcoCar.Models.ServiceManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;


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

        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            dalServiceManagement.CreateService(
                service.PublicationDate,
                service.ExpirationDate,
                service.ReferenceNumber,
                service.IsExpired,
                service.Start,
                service.End,
                service.SelectServiceType
                );

            var selectedValue = service.SelectServiceType;
            ViewBag.ServiceType = selectedValue.ToString();
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
