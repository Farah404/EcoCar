﻿using Braintree;
using EcoCar.Models.FinancialManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using EcoCar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

// ecoCarDevT randomSalt5
// ecocardevteam@gmail.com  random

namespace EcoCar.Controllers
{
    public class FinancialController : Controller
    {
        //Payment
        private readonly IBraintreeService _braintreeService;
        
        private DalFinancialManagement dalFinancialManagement;
        private DalPersonManagement dalPersonManagement;
        

        public FinancialController(IBraintreeService braintreeService)
        {
            dalFinancialManagement = new DalFinancialManagement();
            dalPersonManagement = new DalPersonManagement();
            _braintreeService = braintreeService;
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
            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();  //Genarate a token
            ViewBag.ClientToken = clientToken;

            var data = new EcoStorePurchaseVM
            {
                Id = 2,
                EcoCoinsBatchOnePrice = "20",
                EcoCoinsBatchOne = 40,
                NameOne = "EcoCoinsBatchOne",

                EcoCoinsBatchTwoPrice = 0,
                EcoCoinsBatchTwo = 0,
                NameTwo = "EcoCoinsBatchTwo",

                EcoCoinsBatchThreePrice = 0,
                EcoCoinsBatchThree = 0,
                NameThree = "EcoCoinsBatchThree",


                MonthlySubscriptionPrice = 0,
                MonthlySubscription = 0,
                NameMonth = "MonthlySubscription",

                TrimestrialSubscriptionPrice = 0,
                TrimestrialSubscription = 0,
                NameTrimester = "TrimestrialSubscription",

                SemestrialSubscriptionPrice = 0,
                SemestrialSubscription = 0,
                NameSemester = "SemestrialSubscription",

                Nonce = ""
            };

            return View(data);
        }
        [HttpPost]
        public IActionResult Create(EcoStorePurchaseVM model)
        {
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(model.EcoCoinsBatchOnePrice),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Shared/Error");
            }
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

            //Managing added EcoCoins to EcoWallet based on subscription status
            int ecoWalletEcoCoinsPlusPurchasedAmounts = ecoWallet.EcoCoinsAmount;
            if (ecoWallet.Subscription == true)
            {
                ecoWalletEcoCoinsPlusPurchasedAmounts = ecoWalletEcoCoinsPlusPurchasedAmounts + (shoppingCart.QuantityBatchOne * ecoStore.EcoCoinsBatchOne) + (shoppingCart.QuantityBatchTwo * ecoStore.EcoCoinsBatchTwo) + (shoppingCart.QuantityBatchThree * ecoStore.EcoCoinsBatchThree);
            }
            else
            {
                ecoWalletEcoCoinsPlusPurchasedAmounts = ecoWalletEcoCoinsPlusPurchasedAmounts 
                    + (shoppingCart.QuantityBatchOne * ecoStore.EcoCoinsBatchOne)
                    + (shoppingCart.QuantityBatchTwo * ecoStore.EcoCoinsBatchTwo)
                    + (shoppingCart.QuantityBatchThree * ecoStore.EcoCoinsBatchThree)
                    + (shoppingCart.QuantityMonthlySubscription * ecoStore.MonthlySubscription)
                    + (shoppingCart.QuantityTrimestrialSubscription * ecoStore.TrimestrialSubscription)
                    + (shoppingCart.QuantitySemestrialSubscription * ecoStore.SemestrialSubscription);
            }

            //Defining Subscription system values based on subscription start and expiration dates and duration
            bool subscription = false;

            bool firstMonth = ecoWallet.FirstMonth;
            bool secondMonth = ecoWallet.SecondMonth;
            bool thirdMonth = ecoWallet.ThirdMonth;
            bool fourthMonth = ecoWallet.FourthMonth;
            bool fifthMonth = ecoWallet.FifthMonth;
            bool sixthMonth = ecoWallet.SixthMonth;

            DateTime ecoCoinsFirstMonth = ecoWallet.EcoCoinsFirstMonth;
            DateTime ecoCoinsSecondMonth = ecoWallet.EcoCoinsSecondMonth;
            DateTime ecoCoinsThirdMonth = ecoWallet.EcoCoinsThirdMonth;
            DateTime ecoCoinsFourthMonth = ecoWallet.EcoCoinsFourthMonth;
            DateTime ecoCoinsFitfhMonth = ecoWallet.EcoCoinsFifthMonth;
            DateTime ecoCoinsSixthMonth = ecoWallet.EcoCoinsSixthMonth;

            DateTime subPurchasedExpiration = DateTime.Now;
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
                    firstMonth = true;
                    ecoCoinsFirstMonth = subPurchasedStart;
                    subPurchasedExpiration = ecoWallet.SubscriptionExpiration.AddDays(30);

                }
                else if (shoppingCart.QuantityTrimestrialSubscription != 0)
                {
                    firstMonth = true;
                    secondMonth = true;
                    thirdMonth = true;
                    subPurchasedExpiration = ecoWallet.SubscriptionExpiration.AddDays(90);
                    ecoCoinsFirstMonth = subPurchasedStart;
                    ecoCoinsSecondMonth = subPurchasedStart.AddDays(30);
                    ecoCoinsThirdMonth = subPurchasedStart.AddDays(60);
                }
                else
                {
                    firstMonth = true;
                    secondMonth = true;
                    thirdMonth = true;
                    fourthMonth = true;
                    fifthMonth = true;
                    sixthMonth = true;
                    subPurchasedExpiration = ecoWallet.SubscriptionExpiration.AddDays(180);
                    ecoCoinsFirstMonth = subPurchasedStart;
                    ecoCoinsSecondMonth = subPurchasedStart.AddDays(30);
                    ecoCoinsThirdMonth = subPurchasedStart.AddDays(60);
                    ecoCoinsFourthMonth = subPurchasedStart.AddDays(90);
                    ecoCoinsFitfhMonth = subPurchasedStart.AddDays(120);
                    ecoCoinsSixthMonth = subPurchasedStart.AddDays(150);
                }
            }

            dalFinancialManagement.UpdateEcoWallet(
               ecoWallet.Id,
               ecoWalletEcoCoinsPlusPurchasedAmounts,
               subscription,
               0,
               subPurchasedExpiration,
               subPurchasedStart,
               ecoCoinsFirstMonth,
               firstMonth,
               ecoCoinsSecondMonth,
               secondMonth,
               ecoCoinsThirdMonth,
               thirdMonth,
               ecoCoinsFourthMonth,
               fourthMonth,
               ecoCoinsFitfhMonth,
               fifthMonth,
               ecoCoinsSixthMonth,
               sixthMonth
               );

            dalFinancialManagement.UpdateShoppingCart(shoppingCart.Id, 0, 0, 0, 0, 0, 0, 0);
            return Redirect("/Financial/EcoStore");


        }
        #endregion

    }
}