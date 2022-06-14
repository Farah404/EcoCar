//Class Description
//The Person.cs is the parent class of User.cs and Administrator.cs

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoCar.Models.PersonManagement
{
    public class Person
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ProfilePicturePath { get; set; }
        [NotMapped]
        public IFormFile ProfilePicture { get; set; }
    }
}
