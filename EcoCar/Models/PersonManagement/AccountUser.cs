//Class Description
//The AccountUser.cs contains details used by a user in order to acces to their account

using EcoCar.Models.FinancialManagement;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.PersonManagement
{
    public class AccountUser
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public double UserRating { get; set; }

        [Required]
        public virtual int EcoStatusId
        {
            get
            {
                return (int)this.EcoStatus;
            }
            set
            {
                EcoStatus = (EcoStatusType)value;
            }
        }
        [EnumDataType(typeof(EcoStatusType))]
        public EcoStatusType EcoStatus { get; set; }

        public enum EcoStatusType
        {
            EcoSeed = 0,
            EcoLeaf = 1,
            EcoTree = 2,
            EcoForest = 3
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
