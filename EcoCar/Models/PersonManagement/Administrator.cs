//Class Description
//The Administrator.cs contains all details of a specific administrator 


namespace EcoCar.Models.PersonManagement
{
    public class Administrator
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int PhoneNumberPro { get; set; }
        public string EmailPro { get; set; }

        //Inheritance Class
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }
}
