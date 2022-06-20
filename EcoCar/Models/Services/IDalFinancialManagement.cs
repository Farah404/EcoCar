// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.FinancialManagement;
using System;
using System.Collections.Generic;
using static EcoCar.Models.FinancialManagement.Invoice;

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
        int CreateInvoice(int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType);
        int LastInvoice();
        void UpdateInvoice(int id, int invoiceNumber, string invoiceDescription, DateTime invoiceIssueDate, InvoiceType selectInvoiceType);
        void DeleteInvoice(int id);

        //-------------------------------------------------------------------------------------------------

        //ServiceInvoice
        List<ServiceInvoice> GetAllServiceInvoices();
        public ServiceInvoice GetServiceInvoice(int id);
        int CreateServiceInvoice(int idServiceProvider, int idServiceConsumer, int ecoCoinAmount, int serviceId, int invoiceId);
        void UpdateServiceInvoice(int id, int idServiceProvider, int idServiceConsumer, int ecoCoinAmount, int serviceId, int invoiceId);
        void DeleteServiceInvoice(int id);

        //-------------------------------------------------------------------------------------------------

        //EcoStoreInvoice
        List<EcoStoreInvoice> GetAllEcoStoreInvoices();
        int CreateEcoStoreInvoice(int userId, int invoiceId, int quantityBatchOne,
            int quantityBatchTwo,
            int quantityBatchThree,
            int quantityMonthlySubscription,
            int quantityTrimestrialSubscription,
            int quantitySemestrialSubscription,
            double totalPriceEuros);
        EcoStoreInvoice GetEcoStoreInvoice(int ecoStoreInvoiceId);

        //-------------------------------------------------------------------------------------------------

        //EcoWallet
        List<EcoWallet> GetAllEcoWallets();
        EcoWallet GetUserEcoWallet(int id);
        bool CheckUserFunds(int ecoAmount, int userId);
        int CreateEcoWallet(
            int ecoCoinsAmount,
            bool subscription,
            double ecoCoinsValueEuros,
            DateTime subscriptionExpiration,
            DateTime subscriptionStart,
            DateTime ecoCoinsFirstMonth,
            bool firstMonth,
            DateTime ecoCoinsSecondMonth,
            bool secondMonth,
            DateTime eEcoCoinsThirdMonth,
            bool thirdMonth,
            DateTime ecoCoinsFourthMonth,
            bool fourthMonth,
            DateTime ecoCoinsFifthMonth,
            bool fifthMonth,
            DateTime ecoCoinsSixthMonth,
            bool sixthMonth
            );
        void UpdateEcoWallet(
            int id,
            int ecoCoinsAmount,
            bool subscription,
            double ecoCoinsValueEuros,
            DateTime subscriptionExpiration,
            DateTime subscriptionStart,
            DateTime ecoCoinsFirstMonth,
            bool firstMonth,
            DateTime ecoCoinsSecondMonth,
            bool secondMonth,
            DateTime eEcoCoinsThirdMonth,
            bool thirdMonth,
            DateTime ecoCoinsFourthMonth,
            bool fourthMonth,
            DateTime ecoCoinsFifthMonth,
            bool fifthMonth,
            DateTime ecoCoinsSixthMonth,
            bool sixthMonth
            );
        void DeleteEcoWallet(int id);

        //-------------------------------------------------------------------------------------------------

        //EcoStore
        List<EcoStore> GetAllEcoStores();
        EcoStore GetEcoStore(int id);
        int CreateEcoStore(
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
            );

        void UpdateEcoStore(
            int id,
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
            int semestrialSubscription);

        //-------------------------------------------------------------------------------------------------

        //ShoppingCart
        List<ShoppingCart> GetAllShoppingCarts();
        ShoppingCart GetUserShoppingCart(int userId);

        int CreateShoppingCart(
            int quantityBatchOne,
            int quantityBatchTwo,
            int quantityBatchThree,
            int quantityMonthlySubscription,
            int quantityTrimestrialSubscription,
            int quantitysemestrialSubscription,
            double totalBatchOne,
            double totalBatchTwo,
            double totalBatchThree,
            double totalMonthlySub,
            double totalTrimestrialSub,
            double totalSemestrialSub,
            double totalPriceEuros
            );

        void UpdateShoppingCart(
            int id,
            int quantityBatchOne,
            int quantityBatchTwo,
            int quantityBatchThree,
            int quantityMonthlySubscription,
            int quantityTrimestrialSubscription,
            int quantitysemestrialSubscription,
            double totalBatchOne,
            double totalBatchTwo,
            double totalBatchThree,
            double totalMonthlySub,
            double totalTrimestrialSub,
            double totalSemestrialSub,
            double totalPriceEuros
            );
       
        void DeleteShoppingCart(int id);

        //-------------------------------------------------------------------------------------------------

        //EcoCoinsTransaction

        void EcoCoinsTransactionService(int userProviderId, int userConsumerId, int ecoCoinAmount);
        void EcoCoinsTransactionRequest(int userProviderId, int userConsumerId, int ecoCoinAmount);

        //-------------------------------------------------------------------------------------------------
    }
}