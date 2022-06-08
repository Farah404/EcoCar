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
            string url = "/Service/CreateCarPoolingService" + "?itineraryId=" + itineraryId + "&TrajectoryId=" + trajectoryId + "&serviceId=" + serviceId + "&vehiculeId=" + vehiculeId;
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