//Class Description
//Contains all specific details of a CarPooling service
// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.PersonManagement;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.ServiceManagement
{
    public class CarPoolingService
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public CarPoolingType SelectCarPoolingType { get; set; }
        public enum CarPoolingType
        {
            [Display(Name = "Domicile-travail")]
            HomeToWork,
            [Display(Name = "Domicile-école/université")]
            HomeToSchool,
            [Display(Name = "Evènements")]
            Events,
            [Display(Name = "Voyage")]
            Travel
        }

        public int AvailableSeats { get; set; }
        public bool PetsAllowed { get; set; }
        public bool SmokingAllowed { get; set; }
        public bool MusicAllowed { get; set; }
        public bool ChattingAllowed { get; set; }

        //Foreign Keys
        public int TrajectoryId { get; set; }
        public Trajectory Trajectory { get; set; }
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }

        //Inheritance Class
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

    }
}