using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.MessagingManagement
{
    public class HelpReporting
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string HelpMessageContent { get; set; }

        //Foreign Keys
        public int UserId { get; set; }
        public User User { get; set; }

        //Inheritance Class
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }


    }
}
