//Class Description
//The Vehicule.cs contains all details related to a vehicule owned by a user

using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.PersonManagement
{
    public class Vehicule
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Brand { get; set; }
        public int RegistrationNumber { get; set; }
        public string Model { get; set; }
        public bool Hybrid { get; set; }
        public bool Electric { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TechnicalTestExpiration { get; set; }
        public int AvailableSeats { get; set; }

        //Foreign Keys
        public int InsuranceId { get; set; }
        public Insurance Insurance { get; set; }

    }
}
