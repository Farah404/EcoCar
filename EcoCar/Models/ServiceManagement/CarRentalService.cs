using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.ServiceManagement
{
    public class CarRentalService
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string KeyPickUpAddress { get; set; }
        public string KeyDropOffAddress { get; set; }

        //Foreign Keys
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }

        //Inheritance Class
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}