//Class Description
//The Insurance.cs contains details related to a vehicule

using System;

namespace EcoCar.Models.PersonManagement
{
    public class Insurance
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string InsuranceAgency { get; set; }
        public DateTime InsuranceExpiration { get; set; }
        public string ContractNumber { get; set; }
    }
}
