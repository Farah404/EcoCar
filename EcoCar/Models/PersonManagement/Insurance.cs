//Class Description
//The Insurance.cs contains details related to a vehicule

using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.PersonManagement
{
    public class Insurance
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string InsuranceAgency { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InsuranceExpiration { get; set; }
        public string ContractNumber { get; set; }
    }
}
