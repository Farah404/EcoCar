using System;
using EcoCar.Models.ServiceManagement;
using System.Collections.Generic;
using static EcoCar.Models.ServiceManagement.Service;
using static EcoCar.Models.ServiceManagement.CarPoolingService;
using static EcoCar.Models.ServiceManagement.Trajectory;

namespace EcoCar.Models.Services
{
    public interface IDalServiceManagement : IDisposable
    {
        //Service

        List<Service> GetAllServices();
        int CreateService(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isExpired, DateTime start, DateTime end, ServiceType selectServiceType);
        void UpdateService(int id, DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isExpired, DateTime start, DateTime end, ServiceType selectServiceType);
        void UpdateService(Service service);
        void DeleteService(int id);

        //-------------------------------------------------------------------------------------------------

        //CarPoolingService

        List<CarPoolingService> GetAllCarPoolingServices();
        CarPoolingService CreateCarPoolingService(CarPoolingType selectCarPoolingType, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed, int trajectoryId, int vehiculeId, int serviceId);
        void UpdateCarPoolingService(int id, CarPoolingType selectCarPoolingType, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed, int trajectoryId, int vehiculeId, int serviceId);
        void DeleteCarPoolingService(int id);

        //-------------------------------------------------------------------------------------------------
        //CarRentalService

        List<CarRentalService> GetAllCarRentalServices();
        int CreateCarRentalService(string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId);
        void UpdateCarRentalService(int id, string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId);
        void DeleteCarRentalService(int id);

        //-------------------------------------------------------------------------------------------------

        //ParcelService

        List<ParcelService> GetAllParcelServices();
        int CreateParcelService(int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId);
        void UpdateParcelService(int id, int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId);
        void DeleteParcelService(int id);

        //-------------------------------------------------------------------------------------------------

        //Itinerary

        List<Itinerary> GetAllItineraries();
        int CreateItinerary(string firstStopAddress, string secondStopAddress, string thirdStopAddress);
        void UpdateItinerary(int id, string firstStopAddress, string secondStopAddress, string thirdStopAddress);
        void UpdateItinerary(Itinerary itinerary);
        void DeleteItinerary(int id);

        //-------------------------------------------------------------------------------------------------



        //Trajectory

        List<Trajectory> GetAllTrajectories();
        int CreateTrajectory(int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress, TrajectoryType selectTrajectoryType, int itineraryId);
        void UpdateTrajectory(int id, int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress, TrajectoryType selectTrajectoryType, int itineraryId);
        void UpdateTrajectory(Trajectory trajectory);
        void DeleteTrajectory(int id);

        //-------------------------------------------------------------------------------------------------
    }
}
