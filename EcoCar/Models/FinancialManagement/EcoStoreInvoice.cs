//Class Description
//The EcoStoreInvoice.cs can either represent an invoice for EcoCoins purchase or a subscription

namespace EcoCar.Models.FinancialManagement
{
    public class EcoStoreInvoice
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; }
        public int EcoStoreId { get; set; }
        public EcoStore EcoStore { get; set; }

        //Inheritance Class
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
