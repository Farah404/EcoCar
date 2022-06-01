//Class Description
//The EcoWallet.cs contains the actual EcoCoin credit of the user, if they have a subscription or not and the value of their EcoCoins in euros

namespace EcoCar.Models.FinancialManagement
{
    public class EcoWallet
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public double EcoCoinsAmount { get; set; }
        public bool Subscription { get; set; }
        public double EcoCoinsValueEuros { get; set; }

    }
}
