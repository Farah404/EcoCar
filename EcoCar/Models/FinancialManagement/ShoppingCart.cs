using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.FinancialManagement
{
    public class ShoppingCart
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int QuantityBatchOne { get; set; }
        public int QuantityBatchTwo { get; set; }
        public int QuantityBatchThree {get; set;}
        public int QuantityMonthlySubscription { get; set; }
        public int QuantityTrimestrialSubscription { get; set; }
        public int QuantitySemestrialSubscription { get; set; }
        public double TotalPriceEuros { get; set; }

        //Foreign Key
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
