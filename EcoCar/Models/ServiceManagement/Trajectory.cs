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
        public string PickUpAddress { get; set; }
        public string DeliveryAddress { get; set; }

        public TrajectoryType SelectTrajectoryType { get; set; }
        public enum TrajectoryType
        {
            Regular, Punctual
        }

        //Foreign Keys
        public int ItineraryId { get; set; }
        public Itinerary Itinerary { get; set; }

    }
}