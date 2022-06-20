//Class Description
//User shopping cart values
// MainAuthors : Farah&FrancoisNoel

namespace EcoCar.Models.FinancialManagement
{
    public class ShoppingCart
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int QuantityBatchOne { get; set; }
        public int QuantityBatchTwo { get; set; }
        public int QuantityBatchThree { get; set; }
        public int QuantityMonthlySubscription { get; set; }
        public int QuantityTrimestrialSubscription { get; set; }
        public int QuantitySemestrialSubscription { get; set; }
        public double TotalBatchOne { get; set; }
        public double TotalBatchTwo { get; set; }
        public double TotalBatchThree { get; set; }
        public double TotalMonthlySub { get; set; }
        public double TotalTrimestrialSub { get; set; }
        public double TotalSemestrialSub { get; set; }
        public double TotalPriceEuros { get; set; }
    }
}