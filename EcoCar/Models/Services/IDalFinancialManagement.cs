using EcoCar.Models.FinancialManagement;
using System;
using System.Collections.Generic;

namespace EcoCar.Models.Services
{
    public interface IDalFinancialManagement : IDisposable
    {
        //BankDetails
        List<BankDetails> GetAllBankingDetails();
        int CreateBankDetails(string bankName, string swift, string iban);
        void UpdateBankDetails(int id, string bankName, string swift, string iban);
        void DeleteBankDetails(int id);

        //-------------------------------------------------------------------------------------------------

        //BillingAddress
        List<BillingAddress> GetAllBillingaddresses();
        int CreateBillingAddress(string addressLine, string city, string region, string country, int postalCode);
        void UpdateBillingAddress(int id, string addressLine, string city, string region, string country, int postalCode);
        void DeleteBillingAddress(int id);

        //-------------------------------------------------------------------------------------------------

        //Invoice
        List<Invoice> GetAllInvoices();
        int CreateInvoice(int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate);
        void UpdateInvoice(int id, int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDates);
        void DeleteInvoice(int id);

        //-------------------------------------------------------------------------------------------------

        //ServiceInvoice
        List<ServiceInvoice> GetAllServiceInvoices();
        int CreateServiceInvoice(int iIdServiceProvider, int idServiceConsumer);
        void UpdateServiceInvoice(int id, int iIdServiceProvider, int idServiceConsumers);
        void DeleteServiceInvoice(int id);

        //-------------------------------------------------------------------------------------------------

        //EcoStoreInvoice
        List<EcoStoreInvoice> GetAllEcoStoreInvoices();
        int CreateEcoStoreInvoice();
        void UpdateEcoStoreInvoice(int id);
        void DeleteEcoStoreInvoice(int id);

        //-------------------------------------------------------------------------------------------------

        //Subscription
        List<Subscription> GetAllSubscriptions();
        int CreateSubscription(double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive);
        void UpdateSubscription(int id, double subscriptionCostEuro, DateTime subscriptionExpiration, DateTime subscriptionStart, bool isActive);
        void DeleteSubscription(int id);

        //-------------------------------------------------------------------------------------------------

        //EcoWallet
        List<EcoWallet> GetAllEcoWallets();
        int CreateEcoWallet(double ecoCoinsAmount, bool subscription, double ecoCoinsValueEuros);
        void UpdateEcoWallet(int id, double ecoCoinsAmount, bool subscription, double ecoCoinsValueEuros);
        void DeleteEcoWallet(int id);

        //-------------------------------------------------------------------------------------------------

    }
}
