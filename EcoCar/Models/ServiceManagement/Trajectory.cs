using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.ServiceManagement
{
    public class Trajectory
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int DurationHours { get; set; }
        public int StopNumber { get; set; }
        public int StopsDurationMinutes { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }

        [Required]
        public virtual int TrajectoryTypeId
        {
            get
            {
                return (int)this.Trajectories;
            }
            set
            {
                Trajectories = (TrajectoryType)value;
            }
        }
        [EnumDataType(typeof(TrajectoryType))]
        public TrajectoryType Trajectories { get; set; }
        public enum TrajectoryType
        {
            Regular = 0,
            Punctual = 1
        }

        //Foreign Keys
        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }

    }
}
