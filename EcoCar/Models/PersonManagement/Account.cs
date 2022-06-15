//Class Description
//The Account.cs is the parent class of AccountUser.cs and AccountAdministrator.cs

using System;

namespace EcoCar.Models.PersonManagement
{
    public class Account
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }

    }
}