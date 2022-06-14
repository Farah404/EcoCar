using EcoCar.Models.FinancialManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcoCar.Controllers
{
    public class FinancialController : Controller
    {
        private DalFinancialManagement dalFinancialManagement;
        public FinancialController()
        {
            dalFinancialManagement = new DalFinancialManagement();
        }


        #region Creating bank details and billing address as part of creating an account
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
        #endregion

        #region Ecostore
        public ActionResult EcoStore()
        {
            FinancialViewModel financialViewModel = new FinancialViewModel
            {
                EcoStore = dalFinancialManagement.GetEcoStore(1)
            };
            
            return View(financialViewModel);
        }

        [HttpPost]
        public IActionResult EcoStore(int quantityBatchOne, int quantityBatchTwo,int quantityBatchThree, int quantityMonthlySubscription,int quantityTrimestrialSubscription, int quantitySemestrialSubscription)  
        {
           
            
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                ShoppingCart shoppingCart = dalFinancialManagement.GetUserShoppingCart(userId);
                dalFinancialManagement.UpdateShoppingCart(
                    shoppingCart.Id,
                    quantityBatchOne,
                    quantityBatchTwo,
                    quantityBatchThree,
                    quantityMonthlySubscription,
                    quantityTrimestrialSubscription,
                    quantitySemestrialSubscription,
                    shoppingCart.TotalPriceEuros,
                    shoppingCart.UserId);
                return Redirect("/Financial/EcoStore");
            
        }


        public IActionResult UpdateEcoStore()
        {

            return View();
        }
        #endregion

        #region Payment
        public IActionResult PaymentForm()
        {
            return View();
        }
        #endregion

        #region Invoices: Ecostore and service
        public IActionResult EcoStoreInvoice()
        {
            return View();
        }

        public IActionResult ServiceInvoice()
        {
            return View();
        }
        #endregion

        #region Shopping Cart
        public ActionResult ShoppingCart()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            FinancialViewModel financialViewModel = new FinancialViewModel
            {
                ShoppingCart = dalFinancialManagement.GetUserShoppingCart(userId),
                EcoWallet = dalFinancialManagement.GetUserEcoWallet(userId),
                EcoStore = dalFinancialManagement.GetEcoStore(1)

            };
                
            return View(financialViewModel);
        }
        #endregion
    }
}
