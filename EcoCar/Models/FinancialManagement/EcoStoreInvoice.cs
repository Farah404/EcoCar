//Class Description
//The EcoStoreInvoice.cs can either represent an invoice for EcoCoins purchase or a subscription

using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.FinancialManagement
{
    public class EcoStoreInvoice
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys

        public int UserId { get; set; }
        public User User { get; set; }
        public int QuantityBatchOne { get; set; }
        public int QuantityBatchTwo { get; set; }
        public int QuantityBatchThree { get; set; }
        public int QuantityMonthlySubscription { get; set; }
        public int QuantityTrimestrialSubscription { get; set; }
        public int QuantitySemestrialSubscription { get; set; }
        public double TotalPriceEuros { get; set; }

        //Inheritance Class
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}