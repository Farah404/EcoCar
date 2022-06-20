//Class Description
//The EcoWallet.cs contains the actual EcoCoin credit of the user, if they have a subscription or not and the value of their EcoCoins in euros
//Main Authors : Farah & FrancoisNoel

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
        public DateTime EcoCoinsFirstMonth { get; set; }
        public bool FirstMonth { get; set; }
        public DateTime EcoCoinsSecondMonth { get; set; }
        public bool SecondMonth { get; set; }
        public DateTime EcoCoinsThirdMonth { get; set; }
        public bool ThirdMonth { get; set; }
        public DateTime EcoCoinsFourthMonth { get; set; }
        public bool FourthMonth { get; set; }
        public DateTime EcoCoinsFifthMonth { get; set; }
        public bool FifthMonth { get; set; }
        public DateTime EcoCoinsSixthMonth { get; set; }
        public bool SixthMonth { get; set; }

        //With DateTimes here in account
    }
}