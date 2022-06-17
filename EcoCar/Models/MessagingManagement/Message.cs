//Class Description
//The Message.cs represents a dialogue between two users

using EcoCar.Models.PersonManagement;
using EcoCar.Models.ServiceManagement;

namespace EcoCar.Models.MessagingManagement
{
    public class Message
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string MessageContent { get; set; }

        //Foreign Keys
        public int UserSenderId { get; set; }
        public User UserSender { get; set; }
        public int ServiceConcernedID { get; set; }
        public Service ServiceConcerned { get; set; }
    }
}
