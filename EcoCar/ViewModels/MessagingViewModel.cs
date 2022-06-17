using EcoCar.Models.MessagingManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.ServiceManagement;

namespace EcoCar.ViewModels
{
    public class MessagingViewModel
    {
        public UserReporting userReporting { get; set; }

        public Message Message { get; set; }
        public AdministratorResponse AdministratorResponse { get; set; }
        public Administrator Administrator { get; set; }
        public User User { get; set; }
        public Service Service { get; set; }

    }
}
