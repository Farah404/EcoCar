//Class Description


using EcoCar.Models.PersonManagement;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.ServiceManagement
{
    public class CarPoolingService
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes

        [Required]
        public virtual int CarPoolingTypeId
        {
            get
            {
                return (int)this.CarPooling;
            }
            set
            {
                CarPooling = (CarPoolingType)value;
            }
        }
        [EnumDataType(typeof(CarPoolingType))]
        public CarPoolingType CarPooling { get; set; }
        public enum CarPoolingType
        {
            HomeToWork = 0,
            HomeToSchool = 1,
            Events = 2,
            Travel = 3,
        }

        public int AvailableSeats { get; set; }
        public bool PetsAllowed { get; set; }
        public bool SmokingAllowed { get; set; }
        public bool MusicAllowed { get; set; }
        public bool ChattingAllowed { get; set; }

        //Foreign Keys
        public int VehiculeId { get; set; }
        public Vehicule Vehicule { get; set; }
        public int TrajectoryId { get; set; }
        public Trajectory Trajectory { get; set; }

        //Inheritance Class
        public int ServiceId { get; set; }
        public Service Service { get; set; }

    }
}
