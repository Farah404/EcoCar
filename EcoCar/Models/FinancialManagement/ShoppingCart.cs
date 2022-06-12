using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.FinancialManagement
{
    public class ShoppingCart
    {
        //Primary Key
        public int Id { get; set; }

        //Attribute
        public double EcoPrice { get; set; }


        //Foreign Keys
        public int EcoStoreId { get; set; }
        public EcoStore EcoStore { get; set; }
        public int EcoWalletId { get; set; }
        public EcoWallet EcoWallet { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } 
    }
}
