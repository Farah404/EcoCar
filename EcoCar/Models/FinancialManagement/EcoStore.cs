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
        //Purchase Type
        [Required]
        public virtual int PurchaseId
        {
            get
            {
                return (int)this.Purchase;
            }
            set
            {
                Purchase = (PurchaseType)value;
            }
        }
        [EnumDataType(typeof(PurchaseType))]
        public PurchaseType Purchase { get; set; } 
        public enum PurchaseType
        {
            EcoCoinsBatchOne = 0,
            EcoCoinsBatchTwo = 1,
            EcoCoinsBatchThree = 2,
            MonthlySubscription = 3,
            TrimestrialSubscription = 4,
            SemestrialSubscription = 5
        }
        public double EcoCoinsBatchOnePrice { get; set; }
        public double EcoCoinsBatchTwoPrice { get; set; }
        public double EcoCoinsBatchThreePrice { get; set; }
        public double MonthlySubscriptionPrice { get; set; }
        public double TrimestrialSubscriptionPrice { get; set; }
        public double SemestrialSubscriptionPrice { get; set; }



    }
}
