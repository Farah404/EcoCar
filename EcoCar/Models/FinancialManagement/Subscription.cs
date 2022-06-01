//Class Description
//The Subscription.cs contains all details of a purchased subscription

using System;

namespace EcoCar.Models.FinancialManagement
{
    public class Subscription
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public double SubscriptionCostEuro { get; set; }
        public DateTime SubscriptionExpiration { get; set; }
        public DateTime SubscriptionStart { get; set; }
        public bool IsActive { get; set; }

        //Foreign Keys
        public int EcoStoreId { get; set; }
        public EcoStore EcoStore { get; set; }

    }
}
