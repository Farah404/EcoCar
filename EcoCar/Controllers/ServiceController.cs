using EcoCar.Models.ServiceManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;

namespace EcoCar.Controllers
{
    public class ServiceController : Controller
    {
        private IDalServiceManagement dalServiceManagement;
        private IDalPersonManagement dalPersonManagement;
        public ServiceController()
        {
            dalServiceManagement = new DalServiceManagement();
            dalPersonManagement = new DalPersonManagement();
        }


        public ActionResult SearchService()
        {
            List<Service> services = dalServiceManagement.GetAllServices();

            return View(services.ToList());

        }





        public ActionResult CreateService()
        {

            return View();
        }
 
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                User user = dalPersonManagement.GetAllUsers().FirstOrDefault(r => r.Id == userId);
                int? vehiculeId = user.VehiculeId;
                var selectedValue = service.SelectServiceType;
                ViewBag.ServiceType = selectedValue.ToString();
                int serviceId = dalServiceManagement.CreateService(
                               service.PublicationDate,
                               service.ExpirationDate,
                               service.ReferenceNumber,
                               service.IsExpired,
                               service.Start,
                               service.End,
                               service.SelectServiceType,
                               userId,
                               null
                               ) ;
                                string url = "/Service/CreateItinerary"+ "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                                if (selectedValue == Service.ServiceType.ParcelService)
                                {
                                    url = "/Service/CreateItinerary" + "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                                }
                                else
                                {
                                    if (selectedValue == Service.ServiceType.CarRentalService)
                                    {
                                        url = "/Service/CreateCarRentalService" + "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                                    }
                                }
                                return Redirect(url);
                            }
            return Redirect("/account/loginAccount");
        }
        //Creating Itinerary
        public IActionResult CreateItinerary(int serviceId, int vehiculeId)
        {
            ViewBag.serviceId = serviceId;
            ViewBag.vehiculeId = vehiculeId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateItinerary(Itinerary itinerary, int serviceId, int vehiculeId)
        {
            int itineraryId = dalServiceManagement.CreateItinerary(itinerary.FirtsStopAddress, itinerary.SecondStopAddress, itinerary.ThirdStopAddress);
            string url = "/Service/CreateTrajectory" + "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId +"&itineraryId=" + itineraryId;
            return Redirect(url);
        }
        //Creating Trajectory
        public IActionResult CreateTrajectory(int itineraryId, int serviceId, int vehiculeId)
        {
            ViewBag.itineraryId = itineraryId;
            ViewBag.serviceId = serviceId;
            ViewBag.vehiculeId = vehiculeId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateTrajectory(Trajectory trajectory, int itineraryId, int serviceId, int vehiculeId)
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
            Service service = dalServiceManagement.GetAllServices().FirstOrDefault(r => r.Id == serviceId);
            var selectedValue = service.SelectServiceType;
            string url = "/Service/CreateCarPoolingService" + "?itineraryId=" + itineraryId + "&TrajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
            if (selectedValue == Service.ServiceType.ParcelService)
            {
                url = "/Service/CreateParcelService" + "?itineraryId=" + itineraryId + "&TrajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
            }
            return Redirect(url);
        }
        public IActionResult CreateCarPoolingService(int serviceId, int trajectoryId, int vehiculeId)
        {
            CarPoolingService carPoolingService = new CarPoolingService()
            {
                ServiceId = serviceId,
                TrajectoryId = trajectoryId,
                VehiculeId = vehiculeId
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
                carPoolingService.VehiculeId,
                carPoolingService.TrajectoryId,
                carPoolingService.ServiceId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }
        public ActionResult CreateCarRentalService(int serviceId, int vehiculeId)
        {
            CarRentalService carRentalService = new CarRentalService()
            {
                ServiceId = serviceId,
                VehiculeId = vehiculeId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateCarRentalService(CarRentalService carRentalService)
        {
            dalServiceManagement.CreateCarRentalService(
                carRentalService.KeyPickUpAddress,
                carRentalService.KeyDropOffAddress,
                carRentalService.VehiculeId,
                carRentalService.ServiceId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }
        public ActionResult CreateParcelService(int serviceId, int trajectoryId, int vehiculeId)
        {
            ParcelService parcelService = new ParcelService()
            {
                ServiceId = serviceId,
                TrajectoryId = trajectoryId,
                VehiculeId = vehiculeId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateParcelService(ParcelService parcelService)
        {
            dalServiceManagement.CreateParcelService(
                parcelService.BarCode,
                parcelService.WeightKilogrammes,
                parcelService.AtypicalVolume,
                parcelService.Fragile,
                parcelService.TrajectoryId,
                parcelService.ServiceId,
                parcelService.VehiculeId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }

        //Reserve
        public ActionResult ReserveCarPoolingService()
        {

            return View();

        }
    }
}