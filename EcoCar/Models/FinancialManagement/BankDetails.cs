//Class Description
//The BankDetails.cs contains all the banking references corresponding to one user

namespace EcoCar.Models.FinancialManagement

{
    public class BankDetails
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string BankName { get; set; }
        public string Swift { get; set; }
        public string Iban { get; set; }
    }
}
