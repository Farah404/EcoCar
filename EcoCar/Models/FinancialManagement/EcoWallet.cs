//Class Description
//The EcoWallet.cs contains the actual EcoCoin credit of the user, if they have a subscription or not and the value of their EcoCoins in euros

using System;

namespace EcoCar.Models.FinancialManagement
{
    public class EcoWallet
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int EcoCoinsAmount { get; set; }
        public bool Subscription { get; set; }
        public double EcoCoinsValueEuros { get; set; }
        public DateTime SubscriptionExpiration { get; set; }
        public DateTime SubscriptionStart { get; set; }

    }
}