namespace EcoCar.Models.FinancialManagement
{
    public class EcoStorePurchaseVM : EcoStore
    {
        public string Nonce { get; set; }

        public double TotalEuros { get; set; }
    }
}
