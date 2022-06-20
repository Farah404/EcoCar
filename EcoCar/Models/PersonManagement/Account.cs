//Class Description
//Account values, initially in order to have a parent class for accountUser and accountAdministrator
// MainAuthors : Farah&FrancoisNoel

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
        public DateTime LastLoginDate { get; set; }

    }
}