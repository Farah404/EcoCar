using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.MessagingManagement
{
    public class AdministratorResponse
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string ResponseContent { get; set; }

        //Foreign Keys
        public int ReportingId { get; set; }
        public Reporting Reporting { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AdministratorId { get; set; }
        public Administrator Administrator { get; set; }

        //Inheritance Class
    }
}
