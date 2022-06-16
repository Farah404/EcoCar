using EcoCar.Models.FinancialManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace EcoCar.Controllers
{
    public class FinancialController : Controller
    {
        private DalFinancialManagement dalFinancialManagement;
        private DalPersonManagement dalPersonManagement;
        public FinancialController()
        {
            dalFinancialManagement = new DalFinancialManagement();
            dalPersonManagement = new DalPersonManagement();
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
        public IActionResult EcoStore(int? quantityBatchOne, int? quantityBatchTwo, int? quantityBatchThree, int? quantityMonthlySubscription, int? quantityTrimestrialSubscription, int? quantitySemestrialSubscription)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ShoppingCart shoppingCartToUp = dalFinancialManagement.GetUserShoppingCart(userId);
            dalFinancialManagement.UpdateShoppingCart(
                shoppingCartToUp.Id,
                shoppingCartToUp.QuantityBatchOne + (int)quantityBatchOne,
                shoppingCartToUp.QuantityBatchTwo + (int)quantityBatchTwo,
                shoppingCartToUp.QuantityBatchThree + (int)quantityBatchThree,
                shoppingCartToUp.QuantityMonthlySubscription + (int)quantityMonthlySubscription,
                shoppingCartToUp.QuantityTrimestrialSubscription + (int)quantityTrimestrialSubscription,
                shoppingCartToUp.QuantitySemestrialSubscription + (int)quantitySemestrialSubscription,
                shoppingCartToUp.TotalPriceEuros
                );
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
        public IActionResult EcoStoreInvoice(int id)
        {
            EcoStoreInvoice ecoStoreInvoice = dalFinancialManagement.GetEcoStoreInvoice(id);
            User user = dalPersonManagement.GetUser(ecoStoreInvoice.UserId);
            FinancialViewModel financialViewModel = new FinancialViewModel
            {
                EcoStore = dalFinancialManagement.GetEcoStore(1),
                User = dalPersonManagement.GetUser(ecoStoreInvoice.UserId),
                Account = dalPersonManagement.GetAccount(user.Id),
                ServiceInvoice = dalFinancialManagement.GetServiceInvoice(ecoStoreInvoice.InvoiceId),
                EcoStoreInvoice = ecoStoreInvoice
            };
            return View(financialViewModel);
        }

        public IActionResult ServiceInvoice()
        {
            return View();
        }
        #endregion

        #region Shopping Cart & modifying user details based on their purchase
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

        [HttpPost]
        public IActionResult ShoppingCart(int? quantityBatchOne, int? quantityBatchTwo, int? quantityBatchThree, int? quantityMonthlySubscription, int? quantityTrimestrialSubscription, int? quantitySemestrialSubscription)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ShoppingCart shoppingCart = dalFinancialManagement.GetUserShoppingCart(userId);
            dalFinancialManagement.UpdateShoppingCart(
                shoppingCart.Id,
                (int)quantityBatchOne,
                (int)quantityBatchTwo,
                (int)quantityBatchThree,
                (int)quantityMonthlySubscription,
                (int)quantityTrimestrialSubscription,
                (int)quantitySemestrialSubscription,
                shoppingCart.TotalPriceEuros
                );
            //paiement
            //condition si ok =>

            //Creating EcoStore invoice + reinitialising shopping cart
            int invoiceId = dalFinancialManagement.CreateInvoice(1, null, DateTime.Now, 0);
            dalFinancialManagement.CreateEcoStoreInvoice(userId, invoiceId, shoppingCart.QuantityBatchOne,
            shoppingCart.QuantityBatchTwo,
            shoppingCart.QuantityBatchThree,
            shoppingCart.QuantityMonthlySubscription,
            shoppingCart.QuantityTrimestrialSubscription,
            shoppingCart.QuantitySemestrialSubscription,
            shoppingCart.TotalPriceEuros);


            EcoStore ecoStore = dalFinancialManagement.GetEcoStore(1);
            EcoWallet ecoWallet = dalFinancialManagement.GetUserEcoWallet(userId);


            int purchaseEcoCoins = ecoWallet.EcoCoinsAmount
                + (shoppingCart.QuantityBatchOne * ecoStore.EcoCoinsBatchOne)
                + (shoppingCart.QuantityBatchTwo * ecoStore.EcoCoinsBatchTwo)
                + (shoppingCart.QuantityBatchThree * ecoStore.EcoCoinsBatchThree)
                + (shoppingCart.QuantityMonthlySubscription * ecoStore.MonthlySubscription)
                + (shoppingCart.QuantityTrimestrialSubscription * ecoStore.TrimestrialSubscription)
                + (shoppingCart.QuantitySemestrialSubscription * ecoStore.SemestrialSubscription);


            bool subscription = false;
            DateTime subPurchased = DateTime.Now;
            DateTime subPurchasedStart = DateTime.Now;

            if (shoppingCart.QuantityMonthlySubscription != 0 || shoppingCart.QuantityTrimestrialSubscription != 0 || shoppingCart.QuantitySemestrialSubscription != 0)
            {

                if (ecoWallet.Subscription == true)
                {
                    subscription = true;
                    subPurchasedStart = ecoWallet.SubscriptionExpiration;
                }
                else
                {
                    subscription = true;
                    subPurchasedStart = DateTime.Now;
                }

                if (shoppingCart.QuantityMonthlySubscription != 0)
                {

                    subPurchased = ecoWallet.SubscriptionExpiration.AddDays(30);

                }
                else if (shoppingCart.QuantityTrimestrialSubscription != 0)
                {
                    subPurchased = ecoWallet.SubscriptionExpiration.AddDays(90);
                    DateTime ecoCoinsSecondMonth = DateTime.Now.AddDays(30);
                    DateTime ecoCoinsThirdMonth = DateTime.Now.AddDays(60);
                }
                else
                {
                    subPurchased = ecoWallet.SubscriptionExpiration.AddDays(180);
                    DateTime ecoCoinsSecondMonth = DateTime.Now.AddDays(30);
                    DateTime ecoCoinsThirdMonth = DateTime.Now.AddDays(60);
                    DateTime ecoCoinsFourthMonth = DateTime.Now.AddDays(90);
                    DateTime ecoCoinsFitfhMonth = DateTime.Now.AddDays(120);
                    DateTime ecoCoinsSixthMonth = DateTime.Now.AddDays(150);
                }
            }

            dalFinancialManagement.UpdateEcoWallet(
               ecoWallet.Id,
               purchaseEcoCoins,
               subscription,
               0,
               subPurchased,
               subPurchasedStart);

            dalFinancialManagement.UpdateShoppingCart(shoppingCart.Id, 0, 0, 0, 0, 0, 0, 0);
            return Redirect("/Financial/EcoStore");


        }
        #endregion
    }
}