//Class Description
//The Reporting.cs is the parent class for HelpReporting.cs and fore UserReporting.cs


using System;

namespace EcoCar.Models.MessagingManagement
{
    public class Reporting
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int ReferenceNumber { get; set; }
        public DateTime ReportingDateTime { get; set; }
    }
}
