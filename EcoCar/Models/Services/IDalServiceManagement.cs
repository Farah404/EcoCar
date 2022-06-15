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
        List<Service> GetAllUserServices(int userId);
        int CreateService(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, bool isRequest, ServiceType selectServiceType, int priceEcoCoins, int userProviderId);
        void UpdateService(int id, DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, ServiceType selectServiceType, int priceEcoCoins);
        void UpdateService(Service service);
        void ServiceAvailability(int id);
        void ServiceAvailability(Service service);
        void DeleteService(int id);

        //-------------------------------------------------------------------------------------------------

        //CarPoolingService

        List<CarPoolingService> GetAllCarPoolingServices();
        List<CarPoolingService> GetAllUserCarPoolingServices(int userId);
        CarPoolingService GetCarPoolingService(int id);
        CarPoolingService CreateCarPoolingService(CarPoolingType selectCarPoolingType, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed, int trajectoryId, int vehiculeId, int serviceId);
        void UpdateCarPoolingService(int id, CarPoolingType selectCarPoolingType, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed, int trajectoryId, int vehiculeId, int serviceId);
        void DeleteCarPoolingService(int id);

        //-------------------------------------------------------------------------------------------------
        //CarRentalService

        List<CarRentalService> GetAllCarRentalServices();
        List<CarRentalService> GetAllUserCarRentalServices(int userId);
        CarRentalService GetCarRentalService(int id);
        int CreateCarRentalService(string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId);
        void UpdateCarRentalService(int id, string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId);
        void DeleteCarRentalService(int id);

        //-------------------------------------------------------------------------------------------------

        //ParcelService

        List<ParcelService> GetAllParcelServices();
        List<ParcelService> GetAllUserParcelServices(int userId);
        ParcelService GetParcelService(int id);
        int CreateParcelService(int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId, int vehiculeId);
        void UpdateParcelService(int id, int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId, int vehiculeId);
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

        //Reservation

        List<Reservation> GetAllReservations();
        Reservation CreateReservation(int serviceConsumedId, int serviceUserConsumerId);
        void UpdateReservation(int id, int serviceConsumedId, int serviceUserConsumerId);

        //-------------------------------------------------------------------------------------------------

        //ServiceRequest

        int CreateServiceRequest(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, bool isRequest, ServiceType selectServiceType, int? userProviderId);

        //-------------------------------------------------------------------------------------------------

    }
}