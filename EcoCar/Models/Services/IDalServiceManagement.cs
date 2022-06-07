using System;
using EcoCar.Models.ServiceManagement;
using System.Collections.Generic;

namespace EcoCar.Models.Services
{
    public interface IDalServiceManagement : IDisposable
    {
        //Service

        List<Service> GetAllServices();
        int CreateService(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isExpired, DateTime start, DateTime end, ServiceType selectServiceType);
        void UpdateService(int id, DateTime publicationDateTime, DateTime expirationDateTime, int referenceNumber, bool isExpired, DateTime start, DateTime end, int serviceTypeId);
        void DeleteService(int id);

        //-------------------------------------------------------------------------------------------------

        //CarPoolingService

        List<CarPoolingService> GetAllCarPoolingServices();
        int CreateCarPoolingService(int carPoolingTypeId, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed);
        void UpdateCarPoolingService(int id, int carPoolingTypeId, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed);
        void DeleteCarPoolingService(int id);

        //-------------------------------------------------------------------------------------------------
        //CarRentalService

        List<CarRentalService> GetAllCarRentalServices();
        int CreateCarRentalService(string keyPickUpAdress, string keyDropOffAdress);
        void UpdateCarRentalService(int id, string keyPickUpAdress, string keyDropOffAdress);
        void DeleteCarRentalService(int id);

        //-------------------------------------------------------------------------------------------------

        //CarItinerary

        List<Itinerary> GetAllItineraries();
        int CreateItinerary(string firstStopAddress, string secondStopAddress, string thirdStopAddress);
        void UpdateItinerary(int id, string firstStopAddress, string secondStopAddress, string thirdStopAddress);
        void DeleteItinerary(int id);

        //-------------------------------------------------------------------------------------------------

        //ParcelService

        List<ParcelService> GetAllParcelServices();
        int CreateParcelService(int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile);
        void UpdateParcelService(int id, int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile);
        void DeleteParcelService(int id);

        //-------------------------------------------------------------------------------------------------

        //Trajectory

        List<Trajectory> GetAllTrajectories();
        int CreateTrajectory(int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress);
        void UpdateTrajectory(int id, int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress);
        void DeleteTrajectory(int id);

        //-------------------------------------------------------------------------------------------------
    }
}
