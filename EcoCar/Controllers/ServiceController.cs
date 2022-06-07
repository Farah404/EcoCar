using EcoCar.Models.ServiceManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;


namespace EcoCar.Controllers
{
    public class ServiceController : Controller
    {
        private IDalServiceManagement dalServiceManagement;
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
            var selectedValue = service.SelectServiceType;
            ViewBag.ServiceType = selectedValue.ToString();
           
            dalServiceManagement.CreateService(
                service.PublicationDate,
                service.ExpirationDate,
                service.ReferenceNumber,
                service.IsExpired,
                service.Start,
                service.End,
                service.SelectServiceType
                );
            string url = "/Service/CreateCarRentalService";
            if (selectedValue == Service.ServiceType.ParcelService )
            {
                url = "/Service/CreateParcelService";
            }
            else
            {
                if (selectedValue == Service.ServiceType.CarPoolingService)
                {
                    url = "/Service/CreateCarPoolingService";
                    
                }
            }
            return View(url);
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
