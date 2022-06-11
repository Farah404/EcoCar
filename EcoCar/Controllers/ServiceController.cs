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
                               service.IsAvailable,
                               service.Start,
                               service.End,
                               service.SelectServiceType,
                               userId,
                               0
                               );
                int serviceTypeLinkId = dalServiceManagement.CreateServiceTypeLink();
                dalServiceManagement.UpdateServiceTypeLinkInService(serviceId, serviceTypeLinkId);

                string url = "/Service/CreateItinerary" + "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
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
            string url = "/Service/CreateTrajectory" + "?serviceId=" + serviceId + "&vehiculeId=" + vehiculeId + "&itineraryId=" + itineraryId;
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
            string url = "/Shared/Error";
            if (selectedValue == Service.ServiceType.ParcelService)
            {
                url = "/Service/CreateParcelService" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
            }
            else if (selectedValue == Service.ServiceType.CarPoolingService)
            {
                url = "/Service/CreateCarPoolingService" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
            }
            else
            {
                url = "/Service/CreateCarRentalService" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
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
                carPoolingService.TrajectoryId,
                carPoolingService.VehiculeId,
                carPoolingService.ServiceId
                );
            dalServiceManagement.UpdateServiceTypeLink(carPoolingService.Id);
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
        public ActionResult ReserveCarPoolingService(int? id)
        {
            List<CarPoolingService> carPoolingServices = dalServiceManagement.GetAllCarPoolingServices();
            carPoolingServices = carPoolingServices.Where(x => x.Id == id).ToList();
            return View(carPoolingServices.ToList());

        }

        [HttpPost]
        public IActionResult ReserveCarPoolingService(Reservation reservation, int id)
        {
            CarPoolingService carPoolingService = dalServiceManagement.GetAllCarPoolingServices().FirstOrDefault(x => x.Id == id);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (carPoolingService.AvailableSeats != 0)
            {
                dalServiceManagement.CreateReservation(
                carPoolingService.Service.Id,
                userId
                );
                dalServiceManagement.UpdateCarPoolingService(
                    carPoolingService.Id,
                    carPoolingService.SelectCarPoolingType,
                    (carPoolingService.AvailableSeats) - 1,
                    carPoolingService.PetsAllowed,
                    carPoolingService.SmokingAllowed,
                    carPoolingService.MusicAllowed,
                    carPoolingService.ChattingAllowed,
                    carPoolingService.VehiculeId,
                    carPoolingService.TrajectoryId,
                    carPoolingService.ServiceId);

                dalServiceManagement.ServiceAvailability(
                    carPoolingService.Service.Id
                    );
                string url = "/Home/Index";
                return Redirect(url);
            }
            else
            {
                return Redirect("/Service/SearchService");
            }
        }

        public ActionResult ReserveParcelService(int? id)
        {
            List<ParcelService> parcelServices = dalServiceManagement.GetAllParcelServices();
            parcelServices = parcelServices.Where(x => x.Id == id).ToList();
            return View(parcelServices.ToList());

        }

        [HttpPost]
        public IActionResult ReserveParcelService(Reservation reservation, int id)
        {
            ParcelService parcelService = dalServiceManagement.GetAllParcelServices().FirstOrDefault(x => x.Id == id);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (parcelService.Service.IsAvailable != false)
            {
                dalServiceManagement.CreateReservation(
                    parcelService.Service.Id,
                    userId
                    );

                dalServiceManagement.UpdateParcelService(
                    parcelService.Id,
                    parcelService.BarCode,
                    parcelService.WeightKilogrammes,
                    parcelService.AtypicalVolume,
                    parcelService.Fragile,
                    parcelService.TrajectoryId,
                    parcelService.ServiceId,
                    parcelService.VehiculeId
                    );

                dalServiceManagement.ServiceAvailability(
                    parcelService.Service.Id
                    );


                string url = "/Home/Index";
                return Redirect(url);
            }
            else
            {
                return Redirect("/Service/SearchService");
            }
        }

        public ActionResult ReserveCarRentalService(int? id)
        {
            List<CarRentalService> carRentalServices = dalServiceManagement.GetAllCarRentalServices();
            carRentalServices = carRentalServices.Where(x => x.Id == id).ToList();
            return View(carRentalServices.ToList());

        }

        [HttpPost]
        public IActionResult ReserveCarRentalService(Reservation reservation, int id)
        {
            CarRentalService carRentalService = dalServiceManagement.GetAllCarRentalServices().FirstOrDefault(x => x.Id == id);
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (carRentalService.Service.IsAvailable != false)
            {
                dalServiceManagement.CreateReservation(
                    carRentalService.Service.Id,
                    userId
                    );

                dalServiceManagement.UpdateCarRentalService(
                    carRentalService.Id,
                    carRentalService.KeyPickUpAddress,
                    carRentalService.KeyDropOffAddress,
                    carRentalService.VehiculeId,
                    carRentalService.ServiceId
                    );

                dalServiceManagement.ServiceAvailability(
                    carRentalService.Service.Id
                    );

                string url = "/Home/Index";
                return Redirect(url);
            }
            else
            {
                return Redirect("/Service/SearchService");
            }
        }


    }
}