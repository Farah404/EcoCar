//Class Description
//The AccountAdministrator.cs contains details used by an administrator in order to acces to their account


namespace EcoCar.Models.PersonManagement
{
    public class AccountAdministrator
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string EmployeeCode { get; set; }

        //Inheritance Class
        public int AccountId { get; set; }
        public Account Account { get; set; }

    }
}
