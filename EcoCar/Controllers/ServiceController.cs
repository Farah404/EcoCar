using EcoCar.Models.ServiceManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EcoCar.Controllers
{
    public class ServiceController : Controller
    {
        private IDalServiceManagement dalServiceManagement;
        public ServiceController()
        {
            dalServiceManagement = new DalServiceManagement();
        }
    
        public ActionResult CreateService()
        {
            return View();
        }
        //if (HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        viewModel.Account = dalPersonManagement.GetAccount(userId);
        //        return Redirect("/home/index");
        //    }
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var selectedValue = service.SelectServiceType;
                ViewBag.ServiceType = selectedValue.ToString();
                int serviceId = dalServiceManagement.CreateService(
                   service.PublicationDate,
                   service.ExpirationDate,
                   service.ReferenceNumber,
                   service.IsExpired,
                   service.Start,
                   service.End,
                   service.SelectServiceType
                   );
                string url = "/Service/CreateCarRentalService";
                if (selectedValue == Service.ServiceType.ParcelService)
                {
                    url = "/Service/CreateParcelService";
                }
                else
                {
                    if (selectedValue == Service.ServiceType.CarPoolingService)
                    {
                        url = "/Service/CreateItinerary" + "?serviceId=" + serviceId;
                    }
                }
                return Redirect(url);
            }
            return Redirect("/account/loginAccount");
        }
        //Creating Itinerary
        public IActionResult CreateItinerary(int serviceId)
        {
            ViewBag.serviceId = serviceId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateItinerary(Itinerary itinerary, int serviceId)
        {
            int itineraryId = dalServiceManagement.CreateItinerary(itinerary.FirtsStopAddress, itinerary.SecondStopAddress, itinerary.ThirdStopAddress);
            string url = "/Service/CreateTrajectory" + "?serviceId=" + serviceId + "&itineraryId=" + itineraryId;
            return Redirect(url);
        }
        //Creating Trajectory
        public IActionResult CreateTrajectory(int itineraryId, int serviceId)
        {
            ViewBag.itineraryId = itineraryId;
            ViewBag.serviceId = serviceId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrajectory(Trajectory trajectory, int itineraryId, int serviceId)
        {
            int trajectoryId = dalServiceManagement.CreateTrajectory(
                trajectory.DurationHours,
                trajectory.StopNumber,
                trajectory.StopsDurationMinutes,
                trajectory.PickUpAddress,
                trajectory.DeliveryAddress,
                trajectory.SelectTrajectoryType,
                trajectory.ItineraryId
                );
            string url = "/Service/CreateCarPoolingService" + "?itineraryId=" + itineraryId + "&TrajectoryId=" + trajectoryId + "&serviceId=" + serviceId;
            return Redirect(url);
        }
        public IActionResult CreateCarPoolingService(int serviceId, int trajectoryId)
        {
            CarPoolingService carPoolingService = new CarPoolingService()
            {
                ServiceId = serviceId,
                TrajectoryId = trajectoryId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateCarPoolingService(CarPoolingService carPoolingService)
        {
            dalServiceManagement.CreateCarPoolingService(
                carPoolingService.SelectCarPoolingType,
                carPoolingService.AvailableSeats,
                carPoolingService.PetsAllowed,
                carPoolingService.SmokingAllowed,
                carPoolingService.MusicAllowed,
                carPoolingService.ChattingAllowed,
                carPoolingService.TrajectoryId,
                carPoolingService.ServiceId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }
        public ActionResult CreateCarRentalService()
        {
            return View();
        }
        public ActionResult CreateParcelService()
        {
            return View();
        }

        //Search Service

        public ActionResult SearchService()
        {
            List<Service> services = dalServiceManagement.GetAllServices();
            return View(services.ToList());
        }

        public ActionResult ServicesFilter (string ServiceType)
        {
            var services = dalServiceManagement.GetAllServices();

            if (ServiceType != "all")
            {
                string mainCategory = ServiceType.Split('/')[0];

                services = services.Where(s => s.GetType().Name.Contains(mainCategory)).ToList();

                string subCategory = ServiceType.Split('/')[1];

                if (mainCategory == "carPoolingService")
                {
                    List<CarPoolingService> carPoolingServices = services.Cast<CarPoolingService>().ToList();
                    //carPoolingServices = carPoolingServices.Where(l => l.CarPoolingType.ToString() == subCategory).ToList();
                    services = carPoolingServices.Cast<Service>().ToList();
                }

                else if (mainCategory == "CarRentalService")
                {
                    List<CarRentalService> CarRentalServices = services.Cast<CarRentalService>().ToList();
                    //carRentalServices = CarRentalServices.Where(l => l.CarRentalType.ToString() == subCategory).ToList();
                    services = CarRentalServices.Cast<Service>().ToList();
                }

                else if (mainCategory == "ParcelService")
                {
                    List<ParcelService> ParcelServices = services.Cast<ParcelService>().ToList();
                    //ParcelServices = ParcelServices.Where(l => l.ParcelType.ToString() == subCategory).ToList();
                    services = ParcelServices.Cast<Service>().ToList();
                }


            }

            return View("SearchService", services);
        }


    }
}