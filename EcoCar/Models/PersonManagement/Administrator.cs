//Class Description
//The Administrator.cs contains all details of a specific administrator 


namespace EcoCar.Models.PersonManagement
{
    public class Administrator
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailPro { get; set; }
        public int PhoneNumberPro { get; set; }
        public string EmployeeCode { get; set; }


    }
}
