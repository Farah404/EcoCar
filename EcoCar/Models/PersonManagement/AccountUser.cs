//Class Description
//The AccountUser.cs contains details used by a user in order to acces to their account

using EcoCar.Models.FinancialManagement;

namespace EcoCar.Models.PersonManagement
{
    public class AccountUser
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public double UserRating { get; set; }

        public EcoStatusType SelectEcoStatusType { get; set; }

        public enum EcoStatusType
        {
            EcoSeed,
            EcoLeaf,
            EcoTree,
            EcoForest
        }

        //Foreign Keys
        public int EcoWalletId { get; set; }
        public EcoWallet EcoWallet { get; set; }
        public int? VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }

        //Inheritance Class
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
