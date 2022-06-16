using EcoCar.Models.DataBase;
using EcoCar.Models.FinancialManagement;
using EcoCar.Models.PersonManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcoCar.Models.FinancialManagement.EcoStore;
using static EcoCar.Models.FinancialManagement.Invoice;

namespace EcoCar.Models.Services
{
    public class DalFinancialManagement : IDalFinancialManagement
    {
        private BddContext _bddContext;
        public DalFinancialManagement()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //-------------------------------------------------------------------------------------------------

        #region CRUD Bank Details
        List<BankDetails> IDalFinancialManagement.GetAllBankingDetails()
        {
            return _bddContext.BankingDetails.ToList();
        }

        //Create BankDetails
        int IDalFinancialManagement.CreateBankDetails(string bankName, string swift, string iban)
        {
            BankDetails bankDetails = new BankDetails() { BankName = bankName, Swift = swift, Iban = iban };
            _bddContext.BankingDetails.Add(bankDetails);
            _bddContext.SaveChanges();
            return bankDetails.Id;
        }
        public void CreateBankDetails(BankDetails bankDetails)
        {
            _bddContext.BankingDetails.Update(bankDetails);
            _bddContext.SaveChanges();
        }

        //Update BankDetails
        void IDalFinancialManagement.UpdateBankDetails(int id, string bankName, string swift, string iban)
        {
            BankDetails bankDetails = _bddContext.BankingDetails.Find(id);

            if (bankDetails != null)
            {
                bankDetails.Id = id;
                bankDetails.BankName = bankName;
                bankDetails.Swift = swift;
                bankDetails.Iban = iban;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateBankDetails(BankDetails bankDetails)
        {
            _bddContext.BankingDetails.Update(bankDetails);
            _bddContext.SaveChanges();
        }

        //Delete BankDetails
        void IDalFinancialManagement.DeleteBankDetails(int id)
        {
            BankDetails bankDetails = _bddContext.BankingDetails.Find(id);

            if (bankDetails != null)
            {
                _bddContext.BankingDetails.Remove(bankDetails);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD BillingAddress
        List<BillingAddress> IDalFinancialManagement.GetAllBillingaddresses()
        {
            return _bddContext.BillingAddresses.ToList();
        }

        //Create BillingAddress
        int IDalFinancialManagement.CreateBillingAddress(string addressLine, string city, string region, string country, int postalCode)
        {
            BillingAddress billingAddress = new BillingAddress() { AddressLine = addressLine, City = city, Region = region, Country = country, PostalCode = postalCode };
            _bddContext.BillingAddresses.Add(billingAddress);
            _bddContext.SaveChanges();
            return billingAddress.Id;
        }
        public void CreateBillingAddress(BillingAddress billingAddress)
        {
            _bddContext.BillingAddresses.Update(billingAddress);
            _bddContext.SaveChanges();
        }

        //Update BillingAddress
        void IDalFinancialManagement.UpdateBillingAddress(int id, string addressLine, string city, string region, string country, int postalCode)
        {
            BillingAddress billingAddress = _bddContext.BillingAddresses.Find(id);

            if (billingAddress != null)
            {
                billingAddress.Id = id;
                billingAddress.AddressLine = addressLine;
                billingAddress.City = city;
                billingAddress.Region = region;
                billingAddress.PostalCode = postalCode;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateBillingAddress(BillingAddress billingAddress)
        {
            _bddContext.BillingAddresses.Update(billingAddress);
            _bddContext.SaveChanges();
        }

        //Delete BillingAddress
        void IDalFinancialManagement.DeleteBillingAddress(int id)
        {
            BillingAddress billingAddress = _bddContext.BillingAddresses.Find(id);

            if (billingAddress != null)
            {
                _bddContext.BillingAddresses.Remove(billingAddress);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Invoice
        List<Invoice> IDalFinancialManagement.GetAllInvoices()
        {
            return _bddContext.Invoices.Include(e => e.BillingAddress).ToList();
        }

        public Invoice GetInvoice(int id)
        {
            return _bddContext.Invoices.Include(e => e.BillingAddress).FirstOrDefault(e => e.Id == id);
        }

        //Create Invoice
        int IDalFinancialManagement.CreateInvoice(int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType, int billingAddressId)
        {
            Invoice invoice = new Invoice()
            {
                InvoiceNumber = invoiceNumber,
                InvoiceDescription = invoiceDescription,
                InvoiceIssueDate = invoiceIssueDate,
                SelectInvoiceType = selectInvoiceType,
                BillingAddress = _bddContext.BillingAddresses.First(b => b.Id == billingAddressId)
            };
            _bddContext.Invoices.Add(invoice);
            _bddContext.SaveChanges();
            return invoice.Id;
        }
        public void CreateInvoice(Invoice invoice)
        {
            _bddContext.Invoices.Update(invoice);
            _bddContext.SaveChanges();
        }

        //Update Invoice
        void IDalFinancialManagement.UpdateInvoice(int id, int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType, int billingAddressId)
        {
            Invoice invoice = _bddContext.Invoices.Find(id);

            if (invoice != null)
            {
                invoice.Id = id;
                invoice.InvoiceNumber = invoiceNumber;
                invoice.InvoiceDescription = invoiceDescription;
                invoice.InvoiceIssueDate = invoiceIssueDate;
                invoice.SelectInvoiceType = selectInvoiceType;
                invoice.BillingAddress = _bddContext.BillingAddresses.First(b => b.Id == billingAddressId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateInvoice(Invoice invoice)
        {
            _bddContext.Invoices.Update(invoice);
            _bddContext.SaveChanges();
        }

        //Delete Invoice
        void IDalFinancialManagement.DeleteInvoice(int id)
        {
            Invoice invoice = _bddContext.Invoices.Find(id);

            if (invoice != null)
            {
                _bddContext.Invoices.Remove(invoice);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Service Invoice
        //CRUD ServiceInvoice
        List<ServiceInvoice> IDalFinancialManagement.GetAllServiceInvoices()
        {
            return _bddContext.ServiceInvoices.Include(e => e.Service).Include(e => e.Invoice).ToList();
        }

        public ServiceInvoice GetServiceInvoice(int id)
        {
            return _bddContext.ServiceInvoices.Include(e => e.Service).Include(e => e.Invoice).FirstOrDefault(e => e.Id == id);
        }

        //Create ServiceInvoice
        int IDalFinancialManagement.CreateServiceInvoice(int iIdServiceProvider, int idServiceConsumer, int serviceId, int invoiceId)
        {
            ServiceInvoice serviceInvoice = new ServiceInvoice()
            {
                IdServiceProvider = iIdServiceProvider,
                IdServiceConsumer = idServiceConsumer,
                Service = _bddContext.Services.First(b => b.Id == serviceId),
                Invoice = _bddContext.Invoices.First(b => b.Id == invoiceId),
            };
            _bddContext.ServiceInvoices.Add(serviceInvoice);
            _bddContext.SaveChanges();
            return serviceInvoice.Id;
        }
        public void CreateServiceInvoice(ServiceInvoice serviceInvoice)
        {
            _bddContext.ServiceInvoices.Update(serviceInvoice);
            _bddContext.SaveChanges();
        }

        //Update ServiceInvoice
        void IDalFinancialManagement.UpdateServiceInvoice(int id, int iIdServiceProvider, int idServiceConsumer, int serviceId, int invoiceId)
        {
            ServiceInvoice serviceInvoice = _bddContext.ServiceInvoices.Find(id);

            if (serviceInvoice != null)
            {
                serviceInvoice.Id = id;
                serviceInvoice.IdServiceProvider = iIdServiceProvider;
                serviceInvoice.IdServiceConsumer = idServiceConsumer;
                serviceInvoice.Service = _bddContext.Services.First(b => b.Id == serviceId);
                serviceInvoice.Invoice = _bddContext.Invoices.First(b => b.Id == invoiceId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateServiceInvoice(ServiceInvoice serviceInvoice)
        {
            _bddContext.ServiceInvoices.Update(serviceInvoice);
            _bddContext.SaveChanges();
        }

        //Delete ServiceInvoice
        void IDalFinancialManagement.DeleteServiceInvoice(int id)
        {
            ServiceInvoice serviceInvoice = _bddContext.ServiceInvoices.Find(id);

            if (serviceInvoice != null)
            {
                _bddContext.ServiceInvoices.Remove(serviceInvoice);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD EcoStore Invoice
        List<EcoStoreInvoice> IDalFinancialManagement.GetAllEcoStoreInvoices()
        {
            return _bddContext.EcoStoreInvoices.ToList();
        }

        //Create ServiceInvoice
        int IDalFinancialManagement.CreateEcoStoreInvoice()
        {
            EcoStoreInvoice ecoStoreInvoice = new EcoStoreInvoice() { };
            _bddContext.EcoStoreInvoices.Add(ecoStoreInvoice);
            _bddContext.SaveChanges();
            return ecoStoreInvoice.Id;
        }
        public void CreateEcoStoreInvoice(EcoStoreInvoice ecoStoreInvoice)
        {
            _bddContext.EcoStoreInvoices.Update(ecoStoreInvoice);
            _bddContext.SaveChanges();
        }

        //Update ServiceInvoice
        void IDalFinancialManagement.UpdateEcoStoreInvoice(int id)
        {
            EcoStoreInvoice ecoStoreInvoice = _bddContext.EcoStoreInvoices.Find(id);

            if (ecoStoreInvoice != null)
            {
                ecoStoreInvoice.Id = id;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateEcoStoreInvoice(EcoStoreInvoice ecoStoreInvoice)
        {
            _bddContext.EcoStoreInvoices.Update(ecoStoreInvoice);
            _bddContext.SaveChanges();
        }

        //Delete ServiceInvoice
        void IDalFinancialManagement.DeleteEcoStoreInvoice(int id)
        {
            EcoStoreInvoice ecoStoreInvoice = _bddContext.EcoStoreInvoices.Find(id);

            if (ecoStoreInvoice != null)
            {
                _bddContext.EcoStoreInvoices.Remove(ecoStoreInvoice);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Subscription
        List<Subscription> IDalFinancialManagement.GetAllSubscriptions()
        {
            return _bddContext.Subscriptions.ToList();
        }

        //Create Subscription
        int IDalFinancialManagement.CreateSubscription(double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive)
        {
            Subscription subscription = new Subscription() { SubscriptionCostEuro = subscriptionCostEuro, SubscriptionExpiration = subscriptionExpiration, SubscriptionStart = subscriptionStart, IsActive = isActive };
            _bddContext.Subscriptions.Add(subscription);
            _bddContext.SaveChanges();
            return subscription.Id;
        }
        public void CreateSubscription(Subscription subscription)
        {
            _bddContext.Subscriptions.Update(subscription);
            _bddContext.SaveChanges();
        }

        //Update Subscription
        void IDalFinancialManagement.UpdateSubscription(int id, double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive)
        {
            Subscription subscription = _bddContext.Subscriptions.Find(id);

            if (subscription != null)
            {
                subscription.Id = id;
                subscription.SubscriptionCostEuro = subscriptionCostEuro;
                subscription.SubscriptionExpiration = subscriptionExpiration;
                subscription.SubscriptionStart = subscriptionStart;
                subscription.IsActive = isActive;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateSubscription(Subscription subscription)
        {
            _bddContext.Subscriptions.Update(subscription);
            _bddContext.SaveChanges();
        }

        //Delete Subscription
        void IDalFinancialManagement.DeleteSubscription(int id)
        {
            Subscription subscription = _bddContext.Subscriptions.Find(id);

            if (subscription != null)
            {
                _bddContext.Subscriptions.Remove(subscription);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD EcoWallet
        List<EcoWallet> IDalFinancialManagement.GetAllEcoWallets()
        {
            return _bddContext.EcoWallets.ToList();
        }

        EcoWallet IDalFinancialManagement.GetUserEcoWallet(int id)
        {

            User user = _bddContext.Users.Find(id);
            EcoWallet ecoWallet = _bddContext.EcoWallets.FirstOrDefault(e => e.Id == user.EcoWalletId);
            return ecoWallet;
        }

        //Create EcoWallet
        int IDalFinancialManagement.CreateEcoWallet(double ecoCoinsAmount, bool subscription, double ecoCoinsValueEuros)
        {
            EcoWallet ecoWallet = new EcoWallet() { EcoCoinsAmount = ecoCoinsAmount, Subscription = subscription, EcoCoinsValueEuros = ecoCoinsValueEuros };
            _bddContext.EcoWallets.Add(ecoWallet);
            _bddContext.SaveChanges();
            return ecoWallet.Id;
        }
        public void CreateEcoWallet(EcoWallet ecoWallet)
        {
            _bddContext.EcoWallets.Update(ecoWallet);
            _bddContext.SaveChanges();
        }

        //Update EcoWallet
       public void UpdateEcoWallet(int id, double ecoCoinsAmount, bool subscription, double ecoCoinsValueEuros)
        {
            EcoWallet ecoWallet = _bddContext.EcoWallets.Find(id);

            if (ecoWallet != null)
            {
                ecoWallet.Id = id;
                ecoWallet.EcoCoinsAmount = ecoCoinsAmount;
                ecoWallet.Subscription = subscription;
                ecoWallet.EcoCoinsValueEuros = ecoCoinsValueEuros;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateEcoWallet(EcoWallet ecoWallet)
        {
            _bddContext.EcoWallets.Update(ecoWallet);
            _bddContext.SaveChanges();
        }

       
        //Delete EcoWallet
        void IDalFinancialManagement.DeleteEcoWallet(int id)
        {
            EcoWallet ecoWallet = _bddContext.EcoWallets.Find(id);

            if (ecoWallet != null)
            {
                _bddContext.EcoWallets.Remove(ecoWallet);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD EcoStore
        //CRUD EcoStore
        List<EcoStore> IDalFinancialManagement.GetAllEcoStores()
        {
            return _bddContext.EcoStores.ToList();
        }

        EcoStore IDalFinancialManagement.GetEcoStore(int id)
        {
            return _bddContext.EcoStores.Find(id);
        }

        //Create EcoStore
        int IDalFinancialManagement.CreateEcoStore(
            string nameOne,
            string nameTwo,
            string nameThree,
            string nameMonth,
            string nameTrimester,
            string nameSemester,
            double ecoCoinsBatchOnePrice,
            int ecoCoinsBatchOne,
            double ecoCoinsBatchTwoPrice,
            int ecoCoinsBatchTwo,
            double ecoCoinsBatchThreePrice,
            int ecoCoinsBatchThree,
            double monthlySubscriptionPrice,
            int monthlySubscription,
            double trimestrialSubscriptionPrice,
            int trimestrialSubscription,
            double semestrialSubscriptionPrice,
            int semestrialSubscription

            )
        {
            EcoStore ecoStore = new EcoStore()
            {
                NameOne = nameOne,
                NameTwo = nameTwo,  
                NameThree = nameThree,
                NameMonth = nameMonth,
                NameTrimester = nameTrimester,
                NameSemester = nameSemester,
                EcoCoinsBatchOnePrice = ecoCoinsBatchOnePrice,
                EcoCoinsBatchOne = ecoCoinsBatchOne,
                EcoCoinsBatchTwoPrice = ecoCoinsBatchTwoPrice,
                EcoCoinsBatchTwo = ecoCoinsBatchTwo,
                EcoCoinsBatchThreePrice = ecoCoinsBatchThreePrice,
                EcoCoinsBatchThree = ecoCoinsBatchThree,
                MonthlySubscriptionPrice = monthlySubscriptionPrice,
                MonthlySubscription = monthlySubscription,
                TrimestrialSubscriptionPrice = trimestrialSubscriptionPrice,
                TrimestrialSubscription = trimestrialSubscription,
                SemestrialSubscriptionPrice = semestrialSubscriptionPrice,
                SemestrialSubscription = semestrialSubscription,

            };
            _bddContext.EcoStores.Add(ecoStore);
            _bddContext.SaveChanges();
            return ecoStore.Id;
        }
        public void CreateEcoStore(EcoStore ecoStore)
        {
            _bddContext.EcoStores.Update(ecoStore);
            _bddContext.SaveChanges();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Shopping Cart
        List<ShoppingCart> IDalFinancialManagement.GetAllShoppingCarts()
        {
            return _bddContext.ShoppingCarts.ToList();
        }

        public ShoppingCart GetUserShoppingCart(int userId)
        {
            ShoppingCart shoppingCart = _bddContext.ShoppingCarts.FirstOrDefault(s => s.UserId == userId);
            return (shoppingCart);
        }

        //Create ShoppingCart
        int IDalFinancialManagement.CreateShoppingCart(
            int quantityBatchOne,
            int quantityBatchTwo,
            int quantityBatchThree,
            int quantityMonthlySubscription,
            int quantityTrimestrialSubscription,
            int quantitysemestrialSubscription,
            double totalPriceEuros,
            int userId
            )
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                QuantityBatchOne = quantityBatchOne,
                QuantityBatchTwo = quantityBatchTwo,
                QuantityBatchThree = quantityBatchThree,
                QuantityMonthlySubscription = quantityMonthlySubscription,
                QuantityTrimestrialSubscription = quantityTrimestrialSubscription,
                QuantitySemestrialSubscription = quantitysemestrialSubscription,
                TotalPriceEuros = totalPriceEuros,
                User = _bddContext.Users.First(u => u.Id == userId),
            };
            _bddContext.ShoppingCarts.Add(shoppingCart);
            _bddContext.SaveChanges();
            return shoppingCart.Id;
        }
        public void ShoppingCart(ShoppingCart shoppingCart)
        {
            _bddContext.ShoppingCarts.Update(shoppingCart);
            _bddContext.SaveChanges();
        }

        //Update ShoppingCart
        void IDalFinancialManagement.UpdateShoppingCart(
            int id,
            int quantityBatchOne,
            int quantityBatchTwo,
            int quantityBatchThree,
            int quantityMonthlySubscription,
            int quantityTrimestrialSubscription,
            int quantitysemestrialSubscription,
            double totalPriceEuros,
            int userId
            )
        {
            ShoppingCart shoppingCart = _bddContext.ShoppingCarts.Find(id);

            if (shoppingCart != null)
            {
                shoppingCart.Id = id;
                shoppingCart.QuantityBatchOne = shoppingCart.QuantityBatchOne+quantityBatchOne;
                shoppingCart.QuantityBatchTwo = shoppingCart.QuantityBatchTwo+quantityBatchTwo;
                shoppingCart.QuantityBatchThree = shoppingCart.QuantityBatchThree+quantityBatchThree;
                shoppingCart.QuantityMonthlySubscription = shoppingCart.QuantityMonthlySubscription+quantityMonthlySubscription;
                shoppingCart.QuantityTrimestrialSubscription = shoppingCart.QuantityTrimestrialSubscription+quantityTrimestrialSubscription;
                shoppingCart.QuantitySemestrialSubscription = shoppingCart.QuantitySemestrialSubscription+quantitysemestrialSubscription;
                shoppingCart.TotalPriceEuros = shoppingCart.TotalPriceEuros+totalPriceEuros;
                shoppingCart.User = _bddContext.Users.First(u => u.Id == userId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            _bddContext.ShoppingCarts.Update(shoppingCart);
            _bddContext.SaveChanges();
        }


        //Delete EcoWallet
        void IDalFinancialManagement.DeleteShoppingCart(int id)
        {
            ShoppingCart shoppingCart = _bddContext.ShoppingCarts.Find(id);

            if (shoppingCart != null)
            {
                _bddContext.ShoppingCarts.Remove(shoppingCart);
                _bddContext.SaveChanges();
            }
        }

        //object IDalFinancialManagement.UpdateEcoWallet(EcoWallet consumerEcoWallet)
        //{
        //    _bddContext.consumerEcoWallet.Update(consumerEcoWallet);
        //    _bddContext.SaveChanges();
        //}
        #endregion

    }
}
