//Class Description
//The EcoStore.cs contains all offers that can be purchased by a user as well as the respective price for each offer.

using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.FinancialManagement
{
    public class EcoStore
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public PurchaseType SelectPurchaseType { get; set; } 
        public enum PurchaseType
        {
            EcoCoinsBatchOne,
            EcoCoinsBatchTwo,
            EcoCoinsBatchThree,
            MonthlySubscription,
            TrimestrialSubscription,
            SemestrialSubscription
        }
        public double EcoCoinsBatchOnePrice { get; set; }
        public int EcoCoinsBatchOne { get; set; }
        public double EcoCoinsBatchTwoPrice { get; set; }
        public int EcoCoinsBatchTwo { get; set; }
        public double EcoCoinsBatchThreePrice { get; set; }
        public int EcoCoinsBatchThree { get; set; }
        public double MonthlySubscriptionPrice { get; set; }
        public int MonthlySubscription { get; set; }
        public double TrimestrialSubscriptionPrice { get; set; }
        public int TrimestrialSubscription { get; set; }
        public double SemestrialSubscriptionPrice { get; set; }
        public int SemestrialSubscription { get; set; }



    }
}
