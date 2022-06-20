//Class Description
//Contains all details of a Itinerary
// MainAuthors : Farah&FrancoisNoel

namespace EcoCar.Models.ServiceManagement
{
    public class Itinerary
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public string FirtsStopAddress { get; set; }
        public string SecondStopAddress { get; set; }
        public string ThirdStopAddress { get; set; }

    }
}
