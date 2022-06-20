//Class Description
//Contains all specific details of a Parcel Service
// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.ServiceManagement
{
    public class ParcelService
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int BarCode { get; set; }
        public double WeightKilogrammes { get; set; }
        public bool AtypicalVolume { get; set; }
        public bool Fragile { get; set; }

        //Foreign Keys
        public int TrajectoryId { get; set; }
        public Trajectory Trajectory { get; set; }
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }


        //Inheritance Class
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}