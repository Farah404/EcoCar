using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.ServiceManagement
{
    public class Reservation
    {
        //Primary Key
        public int id { get; set; }

        //Foreign Keys
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int ServiceUserConsumerId { get; set; }
        public User ServiceUserConsumer { get; set; }
    }
}
