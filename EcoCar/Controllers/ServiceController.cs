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

        public ActionResult Service()
        {
            return View();

        }

        public ActionResult CreateService()
        {
            return View();

        }
    }
}
