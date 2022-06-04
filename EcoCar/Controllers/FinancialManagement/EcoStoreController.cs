using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using EcoCar.Models.FinancialManagement;

namespace EcoCar.Controllers.FinancialManagement
{
    public class EcoStoreController : Controller
    {
        private DalFinancialManagement dalFinancialManagement;
        public EcoStoreController()
        {
            dalFinancialManagement = new DalFinancialManagement();
        }
        public IActionResult EcoStore()
        {
            return View();
        }
    }
}
