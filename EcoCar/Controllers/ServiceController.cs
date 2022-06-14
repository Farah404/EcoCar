using EcoCar.Models.PersonManagement;
using EcoCar.Models.ServiceManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcoCar.Models.ServiceManagement.CarPoolingService;
using static EcoCar.Models.ServiceManagement.Service;

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
     //   [HttpPost]   

    public ActionResult SearchService(string selection, DateTime StartingDate, DateTime EndingDate)
        {

            var services = dalServiceManagement.GetAllServices();

            if (selection != null && selection != "All")
            {
                services = services.Where(s => s.SelectServiceType.ToString() == selection).ToList();

                if (selection == "CarPoolingService")
                {
                    List<CarPoolingService> carPoolingServices = dalServiceManagement.GetAllCarPoolingServices();


                    return View("SearchCarpoolingService", carPoolingServices);
                }

                else if (selection == "CarRentalService")
                {
                    List<CarRentalService> CarRentalServices = dalServiceManagement.GetAllCarRentalServices();
                    return View("SearchCarRentalService", CarRentalServices);

                }
                else if (selection == "ParcelService")
                {
                    List<ParcelService> ParcelServices = dalServiceManagement.GetAllParcelServices();
                    return View("SearchParcelService", ParcelServices);
                }

            }

            if (StartingDate.Year != 1)
            {
                services = services.Where(s => s.Start == StartingDate).ToList();
            }


            if (EndingDate.Year != 1)
            {
                services = services.Where(s => s.End == EndingDate).ToList();
            }

            return View(services);
        }

   
            public ActionResult SearchByOption()
        {
            return View();
        }




        public ActionResult SearchCarpoolingService(string selection, string preference, string frequency, int seat)
                    
        {
            List<CarPoolingService> carPoolingServices = dalServiceManagement.GetAllCarPoolingServices();
            List<Trajectory> trajectories = dalServiceManagement.GetAllTrajectories();

            if (selection == "HomeToWork")
            {
                carPoolingServices = carPoolingServices.Where(s => s.SelectCarPoolingType.ToString() == selection).ToList();
                return View("SearchCarPoolingService", carPoolingServices);
            }

            else if (selection == "HomeToSchool")
            {
                carPoolingServices = carPoolingServices.Where(s => s.SelectCarPoolingType.ToString() == selection).ToList();
                return View("SearchCarPoolingService", carPoolingServices);

            }
            else if (selection == "Events")
            {
                carPoolingServices = carPoolingServices.Where(s => s.SelectCarPoolingType.ToString() == selection).ToList();
                return View("SearchCarPoolingService", carPoolingServices);
            }

            else if (selection == "Travel")
            {
                carPoolingServices = carPoolingServices.Where(s => s.SelectCarPoolingType.ToString() == selection).ToList();
                return View("SearchCarPoolingService", carPoolingServices);
            }

            if (preference == "AnimauxDeCompagnie")
            {
                carPoolingServices = carPoolingServices.Where(p => p.PetsAllowed = true).ToList();
            }

            if (preference == "AutorisationDeFumer")
            {
                carPoolingServices = carPoolingServices.Where(p => p.SmokingAllowed = true).ToList();
            }

            if (preference == "MusiqueForte")
            {
                carPoolingServices = carPoolingServices.Where(p => p.MusicAllowed = true).ToList();
            }

            if (preference == "Discussion")
            {
                carPoolingServices = carPoolingServices.Where(p => p.ChattingAllowed = true).ToList();
            }

            if (frequency == "Regulier")
            {
               // carPoolingServices = trajectories.Where(p => p.SelectTrajectoryType=Trajectory.TrajectoryType.Regular).ToList();

                
            }

            if (frequency == "Ponctuel")
            {
                // carPoolingServices = trajectories.Where(p => p.SelectTrajectoryType=Trajectory.TrajectoryType.Punctual).ToList();


            }

            if (seat != 0 && seat > 0)
            {
                carPoolingServices = carPoolingServices.Where(s => s.AvailableSeats == seat).ToList();
                return View("SearchCarpoolingService", carPoolingServices);
            }
            else if (seat < 0)
            {
                return View("Error");
            }
               
            return View(carPoolingServices.ToList());

        }

        public ActionResult SearchParcelService(string preference, string frequency)

        {
           List<ParcelService> parcelServices = dalServiceManagement.GetAllParcelServices();

            
            if (preference == "DimensionsAtypiques")
            {
                parcelServices = parcelServices.Where(p => p.AtypicalVolume = true).ToList();
            }

            if (preference == "Fragile")
            {
                parcelServices = parcelServices.Where(p => p.Fragile = true).ToList();
            }

            if (frequency == "Regulier")
            {
                // parcelServices = trajectories.Where(p => p.SelectTrajectoryType=Trajectory.TrajectoryType.Regular).ToList();


            }

            if (frequency == "Ponctuel")
            {
                // parcelServices = trajectories.Where(p => p.SelectTrajectoryType=Trajectory.TrajectoryType.Punctual).ToList();


            }

            return View(parcelServices.ToList());

        }

        public ActionResult SearchCarRentalService(string selection, string choix, bool preference, int seat, int hours, DateTime PickUpDate)
        {
            List<CarRentalService> CarRentalServices = dalServiceManagement.GetAllCarRentalServices();
            List<Vehicule> Vehicules = dalServiceManagement.GetAllVehicules();
            List<Trajectory> trajectories = dalServiceManagement.GetAllTrajectories();

            //if (PickUpDate.Year != 1)
            //{
            //    services = services.Where(s => s.Start == StartingDate).ToList();
            //}


            if (selection == "Car")
            {

                return View("Car", Vehicules);
            }

            else if (selection == "Bike")
            {

                return View("Bicycle", Vehicules);

            }
            else if (selection == "Bicycle")
            {
                return View("Events", Vehicules);
            }

            else if (selection == "Other")
            {
                return View("Other", Vehicules);
            }

            if (choix == "Marque")
            {
                Vehicules = Vehicules.Where(s => s.Model == choix).ToList();
            }

            //if (preference = "Hybrid")
            //{
            //    Vehicules = Vehicules.Where(s => s.Hybrid = true).ToList();
            //}

            //if (preference = "Electric")
            //{
            //    Vehicules = Vehicules.Where(s => s.Electric = true).ToList();
            //}

            //if (choix == "LieuDeRetrait")
            //{
            //    CarRentalServices = CarRentalServices.Where(p => p.KeyPickUpAddress == LieuDeRetrait).ToList();
            //}

            //if (choix == "LieuDeRetrait")
            //{
            //    CarRentalServices = CarRentalServices.Where(p => p.KeyDropOffAddress == LieuDeRetour).ToList();
            //}
            if (seat != 0 && seat > 0)
            {
                Vehicules = Vehicules.Where(s => s.AvailableSeats == seat).ToList();
                return View("SearchCarRentalService", Vehicules);
            }
            else if (seat < 0)
            {
                return View("Error");
            }

            if (hours != 0 && hours > 0)
            {
                trajectories = trajectories.Where(s => s.DurationHours == hours).ToList();
                return View("SearchCarRentalService", trajectories);
            }
            else if (seat < 0)
            {
                return View("Error");
            }

            return View();

        }

        


    }
}