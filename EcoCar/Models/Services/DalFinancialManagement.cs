using EcoCar.Models.DataBase;
using EcoCar.Models.FinancialManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //-------------------------------------------------------------------------------------------------

        //CRUD BankDetails
        public List<BankDetails> GetAllBankingDetails()
        {
            return _bddContext.BankingDetails.ToList();
        }

        //Create BankDetails
        public int CreateBankDetails(string bankName, string swift, string iban)
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
        public void UpdateBankDetails(int id, string bankName, string swift, string iban)
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
        public void DeleteBankDetails(int id)
        {
            BankDetails bankDetails = _bddContext.BankingDetails.Find(id);

            if (bankDetails != null)
            {
                _bddContext.BankingDetails.Remove(bankDetails);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD BillingAddress
        public List<BillingAddress> GetAllBillingaddresses()
        {
            return _bddContext.BillingAddresses.ToList();
        }

        //Create BillingAddress
        public int CreateBillingAddress(string addressLine, string city, string region, string country, int postalCode)
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
        public void UpdateBillingAddress(int id, string addressLine, string city, string region, string country, int postalCode)
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
        public void DeleteBillingAddress(int id)
        {
            BillingAddress billingAddress = _bddContext.BillingAddresses.Find(id);

            if (billingAddress != null)
            {
                _bddContext.BillingAddresses.Remove(billingAddress);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Invoice
        public List<Invoice> GetAllInvoices()
        {
            return _bddContext.Invoices.Include(e => e.BillingAddress).ToList();
        }

        public Invoice GetInvoice(int id)
        {
            return _bddContext.Invoices.Include(e => e.BillingAddress).FirstOrDefault(e => e.Id == id);
        }

        //Create Invoice
        public int CreateInvoice(int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType, int billingAddressId)
        {
            Invoice invoice = new Invoice() { 
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
        public void UpdateInvoice(int id, int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType, int billingAddressId)
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
        public void DeleteInvoice(int id)
        {
            Invoice invoice = _bddContext.Invoices.Find(id);

            if (invoice != null)
            {
                _bddContext.Invoices.Remove(invoice);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD ServiceInvoice
        public List<ServiceInvoice> GetAllServiceInvoices()
        {
            return _bddContext.ServiceInvoices.Include(e => e.Service).Include(e => e.Invoice).ToList();
        }

        public ServiceInvoice GetServiceInvoice(int id)
        {
            return _bddContext.ServiceInvoices.Include(e => e.Service).Include(e => e.Invoice).FirstOrDefault(e => e.Id == id);
        }

        //Create ServiceInvoice
        public int CreateServiceInvoice(int iIdServiceProvider, int idServiceConsumer, int serviceId, int invoiceId)
        {
            ServiceInvoice serviceInvoice = new ServiceInvoice() { 
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
        public void UpdateServiceInvoice(int id, int iIdServiceProvider, int idServiceConsumer, int serviceId, int invoiceId)
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
        public void DeleteServiceInvoice(int id)
        {
            ServiceInvoice serviceInvoice = _bddContext.ServiceInvoices.Find(id);

            if (serviceInvoice != null)
            {
                _bddContext.ServiceInvoices.Remove(serviceInvoice);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD EcoStoreInvoice
        public List<EcoStoreInvoice> GetAllEcoStoreInvoices()
        {
            return _bddContext.EcoStoreInvoices.ToList();
        }

        //Create ServiceInvoice
        public int CreateEcoStoreInvoice()
        {
            EcoStoreInvoice ecoStoreInvoice = new EcoStoreInvoice() {  };
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
        public void UpdateEcoStoreInvoice(int id)
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
        public void DeleteEcoStoreInvoice(int id)
        {
            EcoStoreInvoice ecoStoreInvoice = _bddContext.EcoStoreInvoices.Find(id);

            if (ecoStoreInvoice != null)
            {
                _bddContext.EcoStoreInvoices.Remove(ecoStoreInvoice);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Subscription
        public List<Subscription> GetAllSubscriptions()
        {
            return _bddContext.Subscriptions.ToList();
        }

        //Create Subscription
        public int CreateSubscription(double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive)
        {
            Subscription subscription = new Subscription() {SubscriptionCostEuro = subscriptionCostEuro, SubscriptionExpiration = subscriptionExpiration, SubscriptionStart = subscriptionStart, IsActive = isActive };
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
        public void UpdateSubscription(int id, double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive)
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
        public void DeleteSubscription(int id)
        {
            Subscription subscription = _bddContext.Subscriptions.Find(id);

            if (subscription != null)
            {
                _bddContext.Subscriptions.Remove(subscription);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD EcoWallet
        public List<EcoWallet> GetAllEcoWallets()
        {
            return _bddContext.EcoWallets.ToList();
        }

        //Create EcoWallet
        public int CreateEcoWallet(double ecoCoinsAmount, bool subscription, double ecoCoinsValueEuros)
        {
            EcoWallet ecoWallet = new EcoWallet() { EcoCoinsAmount = ecoCoinsAmount, Subscription = subscription, EcoCoinsValueEuros = ecoCoinsValueEuros};
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

        //Delete Subscription
        public void DeleteEcoWallet(int id)
        {
            EcoWallet ecoWallet = _bddContext.EcoWallets.Find(id);

            if (ecoWallet != null)
            {
                _bddContext.EcoWallets.Remove(ecoWallet);
                _bddContext.SaveChanges();
            }
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
