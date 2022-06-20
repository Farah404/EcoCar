//Class Description
//The BillingAddress.cs contains the address the user chose as a billing address and not necessarily their postal address
//Main Authors : Farah&FrancoisNoel

namespace EcoCar.Models.FinancialManagement
{
    public class BillingAddress
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public int PostalCode { get; set; }

    }
}
