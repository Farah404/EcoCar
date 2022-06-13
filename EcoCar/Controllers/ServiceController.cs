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
        [HttpPost]   
     public ActionResult SearchByDate (DateTime selection)
        {
            var services = dalServiceManagement.GetAllServices();
            //DateTime date = Selection.HasConversion<DateTime>();
            //string Date = DateTime.Now.ToString("dd-MM-yyyy");
            DateTime StartingDate = DateTime.Today;
            DateTime EndingDate = DateTime.Today;

            if (selection != StartingDate && selection != EndingDate) 
            {
                services = services.Where(s => s.Start >= StartingDate || s.End <= EndingDate).OrderBy(s=>s.Start).ToList();

                    if (! (services != null))
                {
                    return Redirect ("SearchError");
                }
                else
                {
                    return View ("SearchService",services);
                }
            }
                
            else if    (selection != StartingDate)
            {

                services = services.Where(s => s.Start >= StartingDate).OrderBy(s => s.Start).ToList();

                if (!(services != null))
                {
                    return View("SearchError");
                }

                else
                {
                    return View(services.ToList());
                }

            }

            else if (selection != EndingDate)
            {
                services = services.Where(s => s.End <= EndingDate).ToList().OrderBy(s => s.Start).ToList(); ;

                if (!(services != null))
                {
                    return View("SearchError");
                }

                else
                {
                    return View(services.ToList());
                }
               
            }
              
            return View(services.ToList());

        }



    public ActionResult SearchService(string selection, DateTime StartingDate, DateTime EndingDate)
        {
            
            
            
            var services = dalServiceManagement.GetAllServices();



            if (selection != "All"){
                services = services.Where(s => s.SelectServiceType.ToString() == selection).ToList();

            }

            if ( StartingDate.Year != 1)
            {
                services = services.Where(s => s.Start == StartingDate).ToList();
            }


            if (EndingDate.Year != 1)
            {
                services = services.Where(s => s.End == EndingDate).ToList();
            }

            //if (selection != "All")
            //{


            //    if( selection == "CarPoolingService")
            //    {

            //        return View("SearchCarpoolingService", services);
            //    }

            //    else if (selection == "CarRentalService")
            //    {

            //        return View("SearchCarRentalService", services);

            //    }
            //    else if (selection == "ParcelService")
            //    {
            //        return View("SearchParcelService", services);
            //    }


            //}


            return View(services);

        }

        //[HttpPost]
        //public ActionResult SearchService()
        //{
        //    var services = dalServiceManagement.GetAllServices();

        //    if (selection != StartingDate && selection != EndingDate)
        //    {
        //        services = services.Where(s => s.Start >= StartingDate || s.End <= EndingDate).OrderBy(s => s.Start).ToList();

        //        if (!(services != null))
        //        {
        //            return Redirect("SearchError");
        //        }
        //        else
        //        {
        //            return View(services);
        //        }
        //    }

        //}





            public ActionResult SearchByOption()
        {
            return View();
        }




        public ActionResult SearchCarpoolingService(string Selection)
                    
        {
            List<CarPoolingService> carPoolingServices = dalServiceManagement.GetAllCarPoolingServices();

            //if (selection != "all")
            //{
            //    carpoolingservices = carpoolingservices.where(s => s.gettype().name.contains(selection)).tolist();

            //    if (selection == "hometowork")
            //    {
            //        list<hometowork> hometoworks = carpoolingservices.cast<hometowork>().tolist();

            //        carpoolingservices = hometoworks.cast<carpoolingservice>().tolist();
            //    }

            //    else if (selection == "hometoschool")
            //    {

            //        list<hometoschool> hometoschools = carpoolingservices.cast<hometoschool>().tolist();

            //        carpoolingservices = hometoschools.cast<carpoolingservice>().tolist();
            //    }

            //    else if (selection == "events")
            //    {
            //        list<events> eventss = carpoolingservices.cast<events>().tolist();

            //        carpoolingservices = eventss.cast<carpoolingservice>().tolist();
            //    }


            //    else if (selection == "travel")
            //    {
            //        list<travel> travels = carpoolingservices.cast<travel>().tolist();

            //        carpoolingservices = travels.cast<carpoolingservice>().tolist();
            //    }


            //}


            return View(carPoolingServices.ToList());

        }

        public ActionResult SearchParcelService(string Selection)

        {
           List<ParcelService> parcelServices = dalServiceManagement.GetAllParcelServices();

            //if (Selection != "All")
            //{
            //    ParcelServices = parcelServices.Where(s => s.GetType.Name.contains(Selection)).ToList();

            //    if (Selection == "StopNumber")
            //    {

            //    }
            //}



            return View(parcelServices.ToList());

        }

        public ActionResult SearchCarRentalService(string Selection)
        {
            return View();

        }

        //public ActionResult ServicesFilter (string ServiceType)
        //  {
        //      var services = dalServiceManagement.GetAllServices();

        //      if (ServiceType != "all")
        //      {
        //          string mainCategory = ServiceType.Split('/')[0];

        //          services = services.Where(s => s.GetType().Name.Contains(mainCategory)).ToList();

        //          string subCategory = ServiceType.Split('/')[1];

        //          if (mainCategory == "HomeToWork")
        //          {
        //              return View("SearchCarpooling");
        //          }

        //          else if (mainCategory == "CarRentalService")
        //          {
        //              List<CarRentalService> CarRentalServices = services.Cast<CarRentalService>().ToList();
        //              services = CarRentalServices.Cast<Service>().ToList();
        //          }

        //          else if (mainCategory == "ParcelService")
        //          {
        //              List<ParcelService> ParcelServices = services.Cast<ParcelService>().ToList();
        //              //ParcelServices = ParcelServices.Where(l => l.ParcelType.ToString() == subCategory).ToList();
        //              services = ParcelServices.Cast<Service>().ToList();
        //          }


        //      }

        //      return View("SearchService", services);
        //  }


        //  public ActionResult SearchCarPooling (CarPoolingType SelectCarPoolingType)
        //  {
        //      var services = dalServiceManagement.GetAllServices();
        //      List<CarPoolingService> carPoolingServices = services.Cast<CarPoolingService>().ToList();
        //      services = carPoolingServices.Cast<Service>().ToList();

        //      //List<CarPoolingService> carPoolingServices = services.Cast<CarPoolingService>().ToList();

        //      return View ("SearchCarPooling");

        //  }

        //public ActionResult searchParcelService()
        //{
        //    var services = dalServiceManagement.GetAllServices();
        //    list<ParcelService> parcelServices = services.cast<parcelservice>().tolist();
        //    services = parcelServices.Cast<Service>().ToList();



        //    return View("searchParcelService");

        //}


    }
}