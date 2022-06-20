// Main Authors Farah&FrancoisNoel

using Braintree;
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
        // Create banking details & billing address
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

        //Adding EcoStoreView choice into User Shopping Cart 
        [HttpPost]
        public IActionResult EcoStore(int? quantityBatchOne, int? quantityBatchTwo, int? quantityBatchThree, int? quantityMonthlySubscription, int? quantityTrimestrialSubscription, int? quantitySemestrialSubscription)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ShoppingCart shoppingCartToUp = dalFinancialManagement.GetUserShoppingCart(userId);
            EcoStore ecoStore = dalFinancialManagement.GetEcoStore(1);
            if (shoppingCartToUp.QuantityMonthlySubscription == 0 && shoppingCartToUp.QuantityTrimestrialSubscription == 0 && shoppingCartToUp.QuantitySemestrialSubscription == 0)
            {
                dalFinancialManagement.UpdateShoppingCart(
                shoppingCartToUp.Id,
                shoppingCartToUp.QuantityBatchOne + (int)quantityBatchOne,
                shoppingCartToUp.QuantityBatchTwo + (int)quantityBatchTwo,
                shoppingCartToUp.QuantityBatchThree + (int)quantityBatchThree,
                shoppingCartToUp.QuantityMonthlySubscription + (int)quantityMonthlySubscription,
                shoppingCartToUp.QuantityTrimestrialSubscription + (int)quantityTrimestrialSubscription,
                shoppingCartToUp.QuantitySemestrialSubscription + (int)quantitySemestrialSubscription,
                shoppingCartToUp.TotalBatchOne,
                shoppingCartToUp.TotalBatchTwo,
                shoppingCartToUp.TotalBatchThree,
                shoppingCartToUp.TotalMonthlySub,
                shoppingCartToUp.TotalTrimestrialSub,
                shoppingCartToUp.TotalTrimestrialSub,
                shoppingCartToUp.TotalPriceEuros + ((int)quantityBatchOne) * (ecoStore.EcoCoinsBatchOnePrice)
                + ((int)quantityBatchTwo) * (ecoStore.EcoCoinsBatchTwoPrice)
                + ((int)quantityBatchThree) * (ecoStore.EcoCoinsBatchThreePrice)
                + ((int)quantityMonthlySubscription) * (ecoStore.MonthlySubscriptionPrice)
                + ((int)quantityTrimestrialSubscription) * (ecoStore.TrimestrialSubscriptionPrice)
                + ((int)quantitySemestrialSubscription) * (ecoStore.SemestrialSubscriptionPrice)
                );
                return Redirect("/Financial/EcoStore");
            }
            else
            {
                //Restricting user from subscribing from different subscription offers in the same shopping cart
                dalFinancialManagement.UpdateShoppingCart(
                shoppingCartToUp.Id,
                shoppingCartToUp.QuantityBatchOne + (int)quantityBatchOne,
                shoppingCartToUp.QuantityBatchTwo + (int)quantityBatchTwo,
                shoppingCartToUp.QuantityBatchThree + (int)quantityBatchThree,
                shoppingCartToUp.QuantityMonthlySubscription,
                shoppingCartToUp.QuantityTrimestrialSubscription,
                shoppingCartToUp.QuantitySemestrialSubscription,
                shoppingCartToUp.TotalBatchOne,
                shoppingCartToUp.TotalBatchTwo,
                shoppingCartToUp.TotalBatchThree,
                shoppingCartToUp.TotalMonthlySub,
                shoppingCartToUp.TotalTrimestrialSub,
                shoppingCartToUp.TotalTrimestrialSub,
                shoppingCartToUp.TotalPriceEuros + ((int)quantityBatchOne) * (ecoStore.EcoCoinsBatchOnePrice)
                + ((int)quantityBatchTwo) * (ecoStore.EcoCoinsBatchTwoPrice)
                + ((int)quantityBatchThree) * (ecoStore.EcoCoinsBatchThreePrice)
                );
                return Redirect("/Financial/EcoStore");
            }
        }


        public IActionResult UpdateEcoStore()
        {
            FinancialViewModel financialViewModel = new FinancialViewModel
            {
                EcoStore = dalFinancialManagement.GetEcoStore(1)
            };

            return View(financialViewModel);
        }

        [HttpPost]
        public IActionResult UpdateEcoStore(EcoStore ecoStore)
        {
            EcoStore ecoStoreReference = dalFinancialManagement.GetEcoStore(1);
            dalFinancialManagement.UpdateEcoStore(
              ecoStoreReference.Id,
              ecoStoreReference.NameOne,
              ecoStoreReference.NameTwo,
              ecoStoreReference.NameThree,
              ecoStoreReference.NameMonth,
              ecoStoreReference.NameTrimester,
              ecoStoreReference.NameSemester,
              ecoStore.EcoCoinsBatchOnePrice,
              ecoStore.EcoCoinsBatchOne,
              ecoStore.EcoCoinsBatchTwoPrice,
              ecoStore.EcoCoinsBatchTwo,
              ecoStore.EcoCoinsBatchThreePrice,
              ecoStore.EcoCoinsBatchThree,
              ecoStore.MonthlySubscriptionPrice,
              ecoStore.MonthlySubscription,
              ecoStore.TrimestrialSubscriptionPrice,
              ecoStore.TrimestrialSubscription,
              ecoStore.SemestrialSubscriptionPrice,
              ecoStore.SemestrialSubscription
              );
            return Redirect("/Account/AdminHome");
        }
        #endregion

        #region Payment
        public IActionResult PaymentForm(int? userId)
        {
            //BrainTree method for credit card payment - checking bank authentification with BrainTree sandbox

            var gateway = _braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate(); 
            ViewBag.ClientToken = clientToken;

            ViewBag.userId = userId;
            ShoppingCart shoppingCart = dalFinancialManagement.GetUserShoppingCart((int)userId);
            EcoStore ecoStore = dalFinancialManagement.GetEcoStore(1);

            var data = new EcoStorePurchaseVM
            {
                Id = 2,
                EcoCoinsBatchOnePrice = (shoppingCart.QuantityBatchOne) * (ecoStore.EcoCoinsBatchOnePrice),
                EcoCoinsBatchOne = ecoStore.EcoCoinsBatchOne,
                NameOne = ecoStore.NameOne,

                EcoCoinsBatchTwoPrice = (shoppingCart.QuantityBatchTwo) * (ecoStore.EcoCoinsBatchTwoPrice),
                EcoCoinsBatchTwo = ecoStore.EcoCoinsBatchTwo,
                NameTwo = ecoStore.NameTwo,

                EcoCoinsBatchThreePrice = (shoppingCart.QuantityBatchThree) * (ecoStore.EcoCoinsBatchThreePrice),
                EcoCoinsBatchThree = ecoStore.EcoCoinsBatchThree,
                NameThree = ecoStore.NameThree,


                MonthlySubscriptionPrice = (shoppingCart.QuantityMonthlySubscription) * (ecoStore.MonthlySubscriptionPrice),
                MonthlySubscription = ecoStore.MonthlySubscription,
                NameMonth = ecoStore.NameMonth,

                TrimestrialSubscriptionPrice = (shoppingCart.QuantityTrimestrialSubscription) * (ecoStore.TrimestrialSubscriptionPrice),
                TrimestrialSubscription = ecoStore.TrimestrialSubscription,
                NameTrimester = ecoStore.NameTrimester,

                SemestrialSubscriptionPrice = (shoppingCart.QuantitySemestrialSubscription) * (ecoStore.SemestrialSubscriptionPrice),
                SemestrialSubscription = ecoStore.SemestrialSubscription,
                NameSemester = ecoStore.NameSemester,

                Nonce = "",

                TotalEuros = (shoppingCart.QuantityBatchOne) * (ecoStore.EcoCoinsBatchOnePrice)
                + (shoppingCart.QuantityBatchTwo) * (ecoStore.EcoCoinsBatchTwoPrice)
                + (shoppingCart.QuantityBatchThree) * (ecoStore.EcoCoinsBatchThreePrice)
                + (shoppingCart.QuantityMonthlySubscription) * (ecoStore.MonthlySubscriptionPrice)
                + (shoppingCart.QuantityTrimestrialSubscription) * (ecoStore.TrimestrialSubscriptionPrice)
                + (shoppingCart.QuantitySemestrialSubscription) * (ecoStore.SemestrialSubscriptionPrice)
            };

            return View(data);
        }

        [HttpPost]
        public IActionResult Create(EcoStorePurchaseVM model, int userId)
        {
            //BrainTree method for credit card payment
            var gateway = _braintreeService.GetGateway();
            var request = new TransactionRequest
            {
                Amount = Convert.ToDecimal(model.TotalEuros),
                PaymentMethodNonce = model.Nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())

            {
                //Creating EcoStore invoice and reinitializing shopping cart
                ShoppingCart shoppingCart = dalFinancialManagement.GetUserShoppingCart(userId);
                EcoStore ecoStore = dalFinancialManagement.GetEcoStore(1);
                EcoWallet ecoWallet = dalFinancialManagement.GetUserEcoWallet(userId);
                int invoiceNumber = dalFinancialManagement.LastInvoice();
                int invoiceId = dalFinancialManagement.CreateInvoice(invoiceNumber, null, DateTime.Now, 0);
                dalFinancialManagement.CreateEcoStoreInvoice(
                    userId,
                    invoiceId,
                    shoppingCart.QuantityBatchOne,
                    shoppingCart.QuantityBatchTwo,
                    shoppingCart.QuantityBatchThree,
                    shoppingCart.QuantityMonthlySubscription,
                    shoppingCart.QuantityTrimestrialSubscription,
                    shoppingCart.QuantitySemestrialSubscription,
                    shoppingCart.TotalPriceEuros);

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

                //Re-initializing User shopping cart
                dalFinancialManagement.UpdateShoppingCart(shoppingCart.Id, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                return Redirect("/Account/UserProfilePersonal");
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

        public IActionResult ServiceInvoice(int id)
        {
            // Getting Users & Accounts linked to the service
            ServiceInvoice serviceInvoice = dalFinancialManagement.GetServiceInvoice(id);
            User userConsumer = dalPersonManagement.GetUser(serviceInvoice.IdServiceConsumer);
            User userProvider = dalPersonManagement.GetUser(serviceInvoice.IdServiceProvider);
            List<User> usersLinkedToService = new List<User>();
            usersLinkedToService.Add(userConsumer);
            usersLinkedToService.Add(userProvider);
            Account accountConsumer = dalPersonManagement.GetUserAccount(userConsumer.Id);
            Account accountProvider = dalPersonManagement.GetUserAccount(userProvider.Id);
            List<Account> accountLinkedToService = new List<Account>();
            accountLinkedToService.Add(accountConsumer);
            accountLinkedToService.Add(accountProvider);

            FinancialViewModel financialViewModel = new FinancialViewModel
            {
                ServiceInvoice = serviceInvoice,
                Accounts = accountLinkedToService,
                Users = usersLinkedToService,
            };
            return View(financialViewModel);
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
        //Getting shoppingCart values & transmitting them to payment
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
                shoppingCart.TotalBatchOne,
                shoppingCart.TotalBatchTwo,
                shoppingCart.TotalBatchThree,
                shoppingCart.TotalMonthlySub,
                shoppingCart.TotalTrimestrialSub,
                shoppingCart.TotalTrimestrialSub,
                shoppingCart.TotalPriceEuros
                );
            return Redirect("/Financial/PaymentForm" + "?userId=" + userId);
        }

        [HttpPost]
        public IActionResult RefreshShoppingCart(int? quantityBatchOne, int? quantityBatchTwo, int? quantityBatchThree, int? quantityMonthlySubscription, int? quantityTrimestrialSubscription, int? quantitySemestrialSubscription)
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
                shoppingCart.TotalBatchOne,
                shoppingCart.TotalBatchTwo,
                shoppingCart.TotalBatchThree,
                shoppingCart.TotalMonthlySub,
                shoppingCart.TotalTrimestrialSub,
                shoppingCart.TotalTrimestrialSub,
                shoppingCart.TotalPriceEuros
                );
            return Redirect("/Financial/ShoppingCart");
        }
        [HttpPost]
        public IActionResult EmptyShoppingCart()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            ShoppingCart shoppingCart = dalFinancialManagement.GetUserShoppingCart(userId);
            dalFinancialManagement.UpdateShoppingCart(
                shoppingCart.Id, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            return Redirect("/Financial/ShoppingCart");
        }
        #endregion
    }
}