//Class Description
//EcoStorePurchaseVM used in order to proceed to bank authentification through BrainTree
//Main Authors : Farah&FrancoisNoel

namespace EcoCar.Models.FinancialManagement
{
    public class EcoStorePurchaseVM : EcoStore
    {
        public string Nonce { get; set; }

        public double TotalEuros { get; set; }
    }
}
