//Class Description
//The UserReporting.cs contains a message sent by a user to the administrator in which the former reports another user


namespace EcoCar.Models.MessagingManagement
{
    public class UserReporting
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string Comment { get; set; }
        public ReportingReason SelectReportingReason { get; set; }
        public enum ReportingReason
        {
            FalseIdentity = 0,
            FalseAccount = 1,
            InappropriateContent = 2,
            OffensiveLanguage = 3,
            InappropriateProfilePicture = 4
        }

        //Inheritance Class
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }
    }
}
