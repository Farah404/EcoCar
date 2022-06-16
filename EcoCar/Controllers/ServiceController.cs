using EcoCar.Models.ServiceManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using EcoCar.ViewModels;
using EcoCar.Models.FinancialManagement;
using Microsoft.AspNetCore.Http;

namespace EcoCar.Controllers
{
    public class ServiceController : Controller
    {
        private IDalServiceManagement dalServiceManagement;
        private IDalPersonManagement dalPersonManagement;
        private IDalFinancialManagement dalFinancialManagement ;
        public ServiceController()
        {
            dalServiceManagement = new DalServiceManagement();
            dalPersonManagement = new DalPersonManagement();
            dalFinancialManagement = new DalFinancialManagement();
        }

        #region Searching a service in the list of services
        public ActionResult SearchService()
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                Users = dalPersonManagement.GetAllUsers(),
                Accounts = dalPersonManagement.GetAllAccounts(),
                CarPoolingServices = dalServiceManagement.GetAllCarPoolingServices(),
                ParcelServices = dalServiceManagement.GetAllParcelServices(),
                CarRentalServices = dalServiceManagement.GetAllCarRentalServices()

            };


            return View(serviceViewModel);
        }

        [HttpPost]
        public ActionResult SearchService(int id)
        {

            Service service = dalServiceManagement.GetAllServices().FirstOrDefault(s => s.Id == id);

            List<CarPoolingService> carPoolingServices = service.CarPoolingServices.Where(s => s.ServiceId == service.Id).ToList();
            int carPoolingServiceId = 0;
            foreach (CarPoolingService carPoolingService in carPoolingServices)
            {
                carPoolingServiceId = carPoolingService.Id;
            }

            return Redirect("/Service/ReserveCarPoolingService/" + carPoolingServiceId);
        }
        #endregion

        #region Creating a service
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
                if (user.VehiculeId == null)
                {
                    Redirect("/Home/Index");
                    //if we have time : fix the fact that Service controller always redirect to /Home/index in this case whatever path we write
                }
                else
                {
                    int? vehiculeId = user.VehiculeId;
                    var selectedValue = service.SelectServiceType;
                    ViewBag.ServiceType = selectedValue.ToString();


                    int serviceId = dalServiceManagement.CreateService(
                                   service.ServicePrice,
                                   service.PublicationDate,
                                   service.ExpirationDate,
                                   service.ReferenceNumber,
                                   service.IsAvailable,
                                   service.Start,
                                   service.End,
                                   false,
                                   service.SelectServiceType,
                                   userId
                                   );

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
            }
            return Redirect("/account/loginAccount");
        }
        #endregion

        #region Creating an itinerary and a trajectory as a part of the creation of a carpooling service and a parcel service
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
            if (service.IsRequest == false)
            {
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
            }
            else
            {
                if (selectedValue == Service.ServiceType.ParcelService)
                {
                    url = "/Service/CreateParcelRequest" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                }
                else if (selectedValue == Service.ServiceType.CarPoolingService)
                {
                    url = "/Service/CreateCarPoolingRequest" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                }
                else
                {
                    url = "/Service/CreateCarRentalRequest" + "?itineraryId=" + itineraryId + "&trajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
                }
            }
            return Redirect(url);
        }

        #endregion

        #region Creatin carpooling service
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
            string url = "/Home/Index";
            return Redirect(url);
        }
        #endregion

        #region Creating car rental service
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
        #endregion

        #region Creating parcel service
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
        #endregion

        #region Reserving a carpooling service
        //Reserve
        public ActionResult ReserveCarPoolingService(int? id)
        {

            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarPoolingService = dalServiceManagement.GetCarPoolingService((int)id)
                };
                return View(serviceViewModel);

        }

        [HttpPost]
        public IActionResult ReserveCarPoolingService(Reservation reservation, int id)
        {
           
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                EcoWallet consumerEcoWallet = dalFinancialManagement.GetAllEcoWallets().FirstOrDefault(c => c.Id == userId);
                CarPoolingService carPoolingService = dalServiceManagement.GetAllCarPoolingServices().FirstOrDefault(s => s.Id == id);
                EcoWallet providerEcoWallet = dalFinancialManagement.GetAllEcoWallets().FirstOrDefault(p => p.Id == carPoolingService.Service.UserProviderId);
                consumerEcoWallet.EcoCoinsAmount = consumerEcoWallet.EcoCoinsAmount - carPoolingService.Service.ServicePrice;
                providerEcoWallet.EcoCoinsAmount = providerEcoWallet.EcoCoinsAmount + carPoolingService.Service.ServicePrice;

                if (carPoolingService.AvailableSeats == 0)
                {
                    dalServiceManagement.ServiceAvailability(
                    carPoolingService.Service.Id
                    );
                    string url2 = "/Service/CreateCarpoolingRequest";
                    return Redirect(url2);
                }

              
                else if (carPoolingService.AvailableSeats >= 0 && consumerEcoWallet.EcoCoinsAmount != 0 && consumerEcoWallet.EcoCoinsAmount >= carPoolingService.Service.ServicePrice)
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
                        CarPoolingService carPoolingServiceUpdated = dalServiceManagement.GetAllCarPoolingServices().FirstOrDefault(x => x.Id == id);

                        dalFinancialManagement.UpdateEcoWallet(consumerEcoWallet);
                        dalFinancialManagement.UpdateEcoWallet(providerEcoWallet);

                        string url = "/Service/CreateRequest";
                        return Redirect(url);

                    }


            }
            return Redirect("/Account/LoginAccount");
        }
        #endregion

        #region Reserving a parcel service
        public ActionResult ReserveParcelService(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                ParcelService = dalServiceManagement.GetParcelService((int)id)
            };
            return View(serviceViewModel);
        }

        [HttpPost]
        public IActionResult ReserveParcelService(Reservation reservation, int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
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
            return Redirect("/Account/LoginAccount");
        }
        #endregion

        #region Reserving a car rental service
        public ActionResult ReserveCarRentalService(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarRentalService = dalServiceManagement.GetCarRentalService((int)id)
            };
            return View(serviceViewModel);

        }

        [HttpPost]
        public IActionResult ReserveCarRentalService(Reservation reservation, int id)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
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
            return Redirect("/Account/LoginAccount");
        }
        #endregion

        #region Creating a service request
        public IActionResult CreateRequest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRequest(Service serviceRequest)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {

                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                User user = dalPersonManagement.GetAllUsers().FirstOrDefault(r => r.Id == userId);
                int? vehiculeId = user.VehiculeId;
                int serviceRequestId = dalServiceManagement.CreateService(
                               serviceRequest.ServicePrice,
                               serviceRequest.PublicationDate,
                               serviceRequest.ExpirationDate,
                               serviceRequest.ReferenceNumber,
                               serviceRequest.IsAvailable,
                               serviceRequest.Start,
                               serviceRequest.End,
                               true,
                               serviceRequest.SelectServiceType,
                               userId
                               );
                var selectedValue = serviceRequest.SelectServiceType;
                ViewBag.ServiceType = selectedValue.ToString();
                string url = "/Service/CreateItinerary" + "?serviceId=" + serviceRequestId + "&vehiculeId=" + vehiculeId;
                if (selectedValue == Service.ServiceType.ParcelService)
                {
                    url = "/Service/CreateItinerary" + "?serviceId=" + serviceRequestId + "&vehiculeId=" + vehiculeId;
                }
                else
                {
                    if (selectedValue == Service.ServiceType.CarRentalService)
                    {
                        url = "/Service/CreateCarRentalRequest" + "?serviceId=" + serviceRequestId + "&vehiculeId=" + vehiculeId;
                    }
                }
                return Redirect(url);
            }
            return Redirect("/account/loginAccount");
        }
        #endregion

        #region Creating a CarPooling request

        public IActionResult CreateCarpoolingRequest(int serviceId, int trajectoryId, int vehiculeId)
        {
            CarPoolingService carPoolingRequest = new CarPoolingService()

            {
                ServiceId = serviceId,
                TrajectoryId = trajectoryId,
                VehiculeId = vehiculeId
            };
            return View();
        }

        [HttpPost]
        public IActionResult CreateCarPoolingRequest(CarPoolingService carPoolingRequest)
        {
            dalServiceManagement.CreateCarPoolingService(
                carPoolingRequest.SelectCarPoolingType,
                carPoolingRequest.AvailableSeats,
                carPoolingRequest.PetsAllowed,
                carPoolingRequest.SmokingAllowed,
                carPoolingRequest.MusicAllowed,
                carPoolingRequest.ChattingAllowed,
                carPoolingRequest.TrajectoryId,
                carPoolingRequest.VehiculeId,
                carPoolingRequest.ServiceId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }
        #endregion

        #region Creating a Parcel request

        public ActionResult CreateParcelRequest(int serviceId, int trajectoryId, int vehiculeId)
        {
            ParcelService parcelRequest = new ParcelService()
            {
                ServiceId = serviceId,
                TrajectoryId = trajectoryId,
                VehiculeId = vehiculeId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateParcelRequest(ParcelService parcelRequest)
        {
            dalServiceManagement.CreateParcelService(
                parcelRequest.BarCode,
                parcelRequest.WeightKilogrammes,
                parcelRequest.AtypicalVolume,
                parcelRequest.Fragile,
                parcelRequest.TrajectoryId,
                parcelRequest.ServiceId,
                parcelRequest.VehiculeId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }

        #endregion

        #region Creating a CarRental request

        public ActionResult CreateCarRentalRequest(int serviceId, int vehiculeId)
        {
            CarRentalService carRentalRequest = new CarRentalService()
            {
                ServiceId = serviceId,
                VehiculeId = vehiculeId
            };
            return View();
        }
        [HttpPost]
        public IActionResult CreateCarRentalRequest(CarRentalService carRentalRequest)
        {
            dalServiceManagement.CreateCarRentalService(
                carRentalRequest.KeyPickUpAddress,
                carRentalRequest.KeyDropOffAddress,
                carRentalRequest.VehiculeId,
                carRentalRequest.ServiceId
                );
            string url = "/Home/Index";
            return Redirect(url);
        }

        #endregion

        #region RespondingToCarPoolingRequest

        public ActionResult RespondToCarPoolRequest(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarPoolingService = dalServiceManagement.GetCarPoolingService((int)id)
            };
            return View(serviceViewModel);
        }
        #endregion

        #region RespondingToCarRentalRequest

        public ActionResult RespondToCarRentalRequest(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarRentalService = dalServiceManagement.GetCarRentalService((int)id)
            };
            return View(serviceViewModel);
        }
        #endregion

        #region RespondingToParcelRequest

        public ActionResult RespondToParcelRequest(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                ParcelService = dalServiceManagement.GetParcelService((int)id)
            };

            return View(serviceViewModel);
        }
        #endregion

        #region AdminDeleting
        public ActionResult AdminViewCarPoolingService(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarPoolingService = dalServiceManagement.GetCarPoolingService((int)id)
            };
            return View(serviceViewModel);
        }
        [HttpPost]
        public IActionResult AdminViewCarPoolingService(int id)
        {
            CarPoolingService carPoolService = dalServiceManagement.GetCarPoolingService((int)id);
            dalServiceManagement.DeleteService(carPoolService.ServiceId);

            return Redirect("/account/AdminHome");
        }
        public ActionResult AdminViewCarRentalService(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                CarRentalService = dalServiceManagement.GetCarRentalService((int)id)
            };
            return View(serviceViewModel);
        }
        [HttpPost]
        public IActionResult AdminViewCarRentalService(int id)
        {
            CarRentalService carRentalService = dalServiceManagement.GetCarRentalService((int)id);
            dalServiceManagement.DeleteService(carRentalService.ServiceId);

            return Redirect("/account/AdminHome");
        }
        public ActionResult AdminViewParcelService(int? id)
        {
            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                ParcelService = dalServiceManagement.GetParcelService((int)id)
            };

            return View(serviceViewModel);
        }
        [HttpPost]
        public IActionResult AdminViewParcelService(int id)
        {
            ParcelService parcelService = dalServiceManagement.GetParcelService((int)id);
            dalServiceManagement.DeleteService(parcelService.ServiceId);

            return Redirect("/account/AdminHome");
        }
        #endregion
    }
}