//Class Description
//The User.cs contains all details of a specific user wishing to create an account

using EcoCar.Models.FinancialManagement;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.PersonManagement
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "Prénom")]
        [Required]
        [MaxLength(20)]
        public string Prenom { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public Role Role { get; set; }


        //Primary Key
        //public int Id { get; set; }

        //Attributes
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int PhoneNumber { get; set; }
        public int IdentityCardNumber { get; set; }
        public int DrivingPermitNumber { get; set; }


        //Foreign Keys
        public int BankDetailsId { get; set; }
        public BankDetails BankDetails { get; set; }
        public int BillingAddressId { get; set; }
        public BillingAddress BillingAddress { get; set; }

        //Inheritance Class
        public int PersonId { get; set; }
        public Person Person { get; set; }

    }

}

    public enum Role
    {
        Admin,
        ReadWrite,
        ReadOnly
    }


