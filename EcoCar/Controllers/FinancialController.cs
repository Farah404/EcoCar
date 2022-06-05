using EcoCar.Models.FinancialManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoCar.Controllers
{
    public class FinancialController : Controller
    {
        private DalFinancialManagement dalFinancialManagement;
        public FinancialController()
        {
            dalFinancialManagement = new DalFinancialManagement();
        }

        public ActionResult EcoStore()
        {
            return View();
        }
        // Create bAnking details & billing address
        public IActionResult CreateBankDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBankDetails(BankDetails bankDetails)
        {
            dalFinancialManagement.CreateBankDetails(bankDetails.BankName, bankDetails.Swift, bankDetails.Iban);
            return Redirect("/Financial/CreateBillingAddress");
        }
        public IActionResult CreateBillingAddress()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateBillingAddress(BillingAddress billingAddress)
        {
            dalFinancialManagement.CreateBillingAddress(billingAddress.AddressLine, billingAddress.City, billingAddress.Region, billingAddress.Country, billingAddress.PostalCode) ;
            return Redirect("/Account/CreateUser");
        }
    }
}
