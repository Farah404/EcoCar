using EcoCar.Models.FinancialManagement;
using EcoCar.Models.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult CreateBankDetails(int personId)
        {
            ViewBag.PersonId = personId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateBankDetails(BankDetails bankDetails, int personId)
        {
            int bankDetailsId = dalFinancialManagement.CreateBankDetails(bankDetails.BankName, bankDetails.Swift, bankDetails.Iban);
            string url = "/Financial/CreateBillingAddress" + "?personId=" + personId + "&bankDetailsId=" + bankDetailsId;
            return Redirect(url);
        }

        public IActionResult CreateBillingAddress(int personId, int bankDetailsId)
        {
            ViewBag.BankDetailsId = bankDetailsId;
            ViewBag.PersonId = personId;
            return View();
        }
        [HttpPost]
        public IActionResult CreateBillingAddress(BillingAddress billingAddress, int bankDetailsId, int personId)
        {

            int billingAddressId = dalFinancialManagement.CreateBillingAddress(billingAddress.AddressLine, billingAddress.City, billingAddress.Region, billingAddress.Country, billingAddress.PostalCode);
            string url = "/Account/CreateUser" + "?personId=" + personId + "&bankDetailsId=" + bankDetailsId + "&billingAddressId=" + billingAddressId; ;
            return Redirect(url);
        }

        public IActionResult ListBankDetails()
        {
            {
                List<BankDetails> listBankDetails = dalFinancialManagement.GetAllBankingDetails();
                return View(listBankDetails);
            }
        }
        public IActionResult ListBillingAddress()
        {
            {
                List<BillingAddress> listBillingAddress = dalFinancialManagement.GetAllBillingaddresses();
                return View(listBillingAddress);
            }
        }
    }
}
