﻿// MainAuthors : Farah&FrancoisNoel

using EcoCar.Models.DataBase;
using EcoCar.Models.ServiceManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using static EcoCar.Models.ServiceManagement.CarPoolingService;
using static EcoCar.Models.ServiceManagement.Service;
using static EcoCar.Models.ServiceManagement.Trajectory;

namespace EcoCar.Models.Services
{
    public class DalServiceManagement : IDalServiceManagement
    {
        private BddContext _bddContext;
        public DalServiceManagement()
        {
            _bddContext = new BddContext();
        }

        //-------------------------------------------------------------------------------------------------

        #region CRUD Service 

        public List<Service> GetAllServices()
        {
            List<Service> services = _bddContext.Services.Include(s => s.UserProvider).ToList();
            return services;
        }
        public List<Service> GetAllUserServices(int userId)
        {
            List<Service> servicesOfUser = _bddContext.Services.Include(s => s.UserProvider).Where(s => s.Id == userId).ToList();
            return servicesOfUser;
        }
        public Service GetService(int serviceId)
        {
            return _bddContext.Services.Include(s => s.UserProvider).FirstOrDefault(s => s.Id == serviceId);
        }
        public Service GetServiceFromReferenceNumber(int referenceNumber)
        {
            return _bddContext.Services.Include(s => s.UserProvider).FirstOrDefault(s=> s.ReferenceNumber == referenceNumber);
        }

        public int CreateService(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, bool isRequest, ServiceType selectServiceType, int priceEcoCoins, int userProviderId)
        {
            Service service = new Service()
            {
                PublicationDate = publicationDate,
                ExpirationDate = expirationDate,
                ReferenceNumber = referenceNumber,
                IsAvailable = true,
                Start = start,
                End = end,
                IsRequest = isRequest,
                SelectServiceType = selectServiceType,
                PriceEcoCoins = priceEcoCoins,
                UserProvider = _bddContext.Users.First(s => s.Id == userProviderId)
            };
            _bddContext.Services.Add(service);
            _bddContext.SaveChanges();
            return service.Id;
        }
        public void CreateService(Service service)
        {
            _bddContext.Services.Update(service);
            _bddContext.SaveChanges();
        }
        public void UpdateService(int id, DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, ServiceType selectServiceType, int priceEcoCoins, int userProviderId)
        {
            Service service = _bddContext.Services.Find(id);

            if (service != null)
            {
                service.Id = id;
                service.PublicationDate = publicationDate;
                service.ExpirationDate = expirationDate;
                service.ReferenceNumber = referenceNumber;
                service.IsAvailable = isAvailable;
                service.Start = start;
                service.End = end;
                service.PriceEcoCoins = priceEcoCoins;
                service.SelectServiceType = selectServiceType;
                service.UserProviderId = userProviderId;

                _bddContext.SaveChanges();
            }

        }

        public void UpdateService(Service service)
        {
            _bddContext.Services.Update(service);
            _bddContext.SaveChanges();
        }


        public void ServiceAvailability(int id)
        {
            Service service = _bddContext.Services.Find(id);
            if (service != null)
            {
                service.Id = id;
                service.IsAvailable = false;

                _bddContext.SaveChanges();
            }
        }

        public void ServiceAvailability(Service service)
        {
            _bddContext.Services.Update(service);
            _bddContext.SaveChanges();
        }


        public void DeleteService(int id)
        {
            Service service = _bddContext.Services.Find(id);
            if (service != null)
            {
                _bddContext.Services.Remove(service);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD CarPoolingService

        public List<CarPoolingService> GetAllCarPoolingServices()
        {
            return _bddContext.CarPoolingServices.Include(e => e.Trajectory).Include(e => e.Service).ToList();
        }
        public List<CarPoolingService> GetAllUserCarPoolingServices(int userId)
        {
            List<CarPoolingService> userCarPoolingServices = _bddContext.CarPoolingServices.Include(e => e.Trajectory).Include(e => e.Service).Where(e => e.Service.UserProviderId == userId).ToList();
            return userCarPoolingServices;
        }

        public CarPoolingService GetCarPoolingService(int id)
        {
            return _bddContext.CarPoolingServices.Include(e => e.Trajectory).Include(e => e.Service).FirstOrDefault(e => e.Id == id);
        }

        public CarPoolingService CreateCarPoolingService(
            CarPoolingType selectCarPoolingType,
            int avalaibleSeats,
            bool petsAllowed,
            bool smokingAllowed,
            bool musicAllowed,
            bool chattingAllowed,
            int trajectoryId,
            int vehiculeId,
            int serviceId
            )
        {
            CarPoolingService carPoolingService = new CarPoolingService()
            {
                SelectCarPoolingType = selectCarPoolingType,
                AvailableSeats = avalaibleSeats,
                PetsAllowed = petsAllowed,
                SmokingAllowed = smokingAllowed,
                MusicAllowed = musicAllowed,
                ChattingAllowed = chattingAllowed,
                Trajectory = _bddContext.Trajectories.First(b => b.Id == trajectoryId),
                Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId),
                Service = _bddContext.Services.First(b => b.Id == serviceId)
            };
            _bddContext.CarPoolingServices.Add(carPoolingService);
            _bddContext.SaveChanges();
            return carPoolingService;
        }
        public void CreateCarPoolingService(CarPoolingService service)
        {
            _bddContext.CarPoolingServices.Update(service);
            _bddContext.SaveChanges();
        }
        public void UpdateCarPoolingService(
            int id,
            CarPoolingType selectCarPoolingType,
            int avalaibleSeats,
            bool petsAllowed,
            bool smokingAllowed,
            bool musicAllowed,
            bool chattingAllowed,
            int vehiculeId,
            int trajectoryId,
            int serviceId)
        {
            CarPoolingService service = _bddContext.CarPoolingServices.Find(id);

            if (service != null)
            {
                service.Id = id;
                service.SelectCarPoolingType = selectCarPoolingType;
                service.AvailableSeats = avalaibleSeats;
                service.PetsAllowed = petsAllowed;
                service.SmokingAllowed = smokingAllowed;
                service.MusicAllowed = musicAllowed;
                service.ChattingAllowed = chattingAllowed;
                service.Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId);
                service.Trajectory = _bddContext.Trajectories.First(b => b.Id == trajectoryId);
                service.Service = _bddContext.Services.First(b => b.Id == serviceId);
                _bddContext.SaveChanges();
            }

        }
        public void UpdateCarPoolingService(CarPoolingService carPoolingService)
        {
            _bddContext.CarPoolingServices.Update(carPoolingService);
            _bddContext.SaveChanges();
        }

        public void DeleteCarPoolingService(int id)
        {
            CarPoolingService carPoolingService = _bddContext.CarPoolingServices.Find(id);
            if (carPoolingService != null)
            {
                _bddContext.CarPoolingServices.Remove(carPoolingService);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD CarRentalService

        public List<CarRentalService> GetAllCarRentalServices()
        {
            return _bddContext.CarRentalServices.Include(e => e.Vehicule).Include(e => e.Service).ToList();
        }
        public List<CarRentalService> GetAllUserCarRentalServices(int userId)
        {
            return _bddContext.CarRentalServices.Include(e => e.Vehicule).Include(e => e.Service).Where(e => e.Service.UserProviderId == userId).ToList();
        }
        public CarRentalService GetCarRentalService(int id)
        {
            return _bddContext.CarRentalServices.Include(e => e.Vehicule).Include(e => e.Service).FirstOrDefault(e => e.Id == id);
        }

        public int CreateCarRentalService(string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId)
        {
            CarRentalService carRentalService = new CarRentalService()
            {
                KeyPickUpAddress = keyPickUpAdress,
                KeyDropOffAddress = keyDropOffAdress,
                Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId),
                Service = _bddContext.Services.First(b => b.Id == serviceId)
            };

            _bddContext.CarRentalServices.Add(carRentalService);
            _bddContext.SaveChanges();
            return carRentalService.Id;
        }
        public void CreateCarRentalService(CarRentalService carRentalService)
        {
            _bddContext.CarRentalServices.Update(carRentalService);
            _bddContext.SaveChanges();
        }
        public void UpdateCarRentalService(int id, string keyPickUpAdress, string keyDropOffAdress, int vehiculeId, int serviceId)
        {
            CarRentalService carRentalService = _bddContext.CarRentalServices.Find(id);

            if (carRentalService != null)
            {
                carRentalService.Id = id;
                carRentalService.KeyPickUpAddress = keyPickUpAdress;
                carRentalService.KeyDropOffAddress = keyDropOffAdress;
                carRentalService.VehiculeId = vehiculeId;
                carRentalService.Service = _bddContext.Services.First(b => b.Id == serviceId);
                _bddContext.SaveChanges();
            }

        }
        public void UpdateCarRentalService(CarRentalService carRentalService)
        {
            _bddContext.CarRentalServices.Update(carRentalService);
            _bddContext.SaveChanges();
        }

        public void DeleteCarRentalService(int id)
        {
            CarRentalService carRentalService = _bddContext.CarRentalServices.Find(id);
            if (carRentalService != null)
            {
                _bddContext.CarRentalServices.Remove(carRentalService);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD ParcelService

        public List<ParcelService> GetAllParcelServices()
        {
            return _bddContext.ParcelServices.Include(e => e.Trajectory).Include(e => e.Service).ToList();
        }
        public List<ParcelService> GetAllUserParcelServices(int userId)
        {
            return _bddContext.ParcelServices.Include(e => e.Trajectory).Include(e => e.Service).Where(e => e.Service.UserProviderId == userId).ToList();
        }
        public ParcelService GetParcelService(int id)
        {
            return _bddContext.ParcelServices.Include(e => e.Trajectory).Include(e => e.Service).FirstOrDefault(e => e.Id == id);
        }

        public int CreateParcelService(int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId, int vehiculeId)
        {
            ParcelService parcelService = new ParcelService()
            {
                BarCode = barCode,
                WeightKilogrammes = weightKilogrammes,
                AtypicalVolume = atypicalVolume,
                Fragile = fragile,
                Trajectory = _bddContext.Trajectories.First(b => b.Id == trajectoryId),
                Service = _bddContext.Services.First(b => b.Id == serviceId),
                Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId)
            };

            _bddContext.ParcelServices.Add(parcelService);
            _bddContext.SaveChanges();
            return parcelService.Id;
        }
        public void CreateParcelService(ParcelService parcelService)
        {
            _bddContext.ParcelServices.Update(parcelService);
            _bddContext.SaveChanges();
        }
        public void UpdateParcelService(int id, int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile, int trajectoryId, int serviceId, int vehiculeId)
        {
            ParcelService parcelService = _bddContext.ParcelServices.Find(id);

            if (parcelService != null)
            {
                parcelService.Id = id;
                parcelService.BarCode = barCode;
                parcelService.WeightKilogrammes = weightKilogrammes;
                parcelService.AtypicalVolume = atypicalVolume;
                parcelService.Fragile = fragile;
                parcelService.Trajectory = _bddContext.Trajectories.First(b => b.Id == trajectoryId);
                parcelService.Service = _bddContext.Services.First(b => b.Id == serviceId);
                parcelService.Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId);

                _bddContext.SaveChanges();
            }

        }
        public void UpdateParcelService(ParcelService parcelService)
        {
            _bddContext.ParcelServices.Update(parcelService);
            _bddContext.SaveChanges();
        }

        public void DeleteParcelService(int id)
        {
            ParcelService parcelService = _bddContext.ParcelServices.Find(id);
            if (parcelService != null)
            {
                _bddContext.ParcelServices.Remove(parcelService);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Itinerary

        public List<Itinerary> GetAllItineraries()
        {
            return _bddContext.Itineraries.ToList();
        }
        public int CreateItinerary(string firstStopAddress, string secondStopAddress, string thirdStopAddress)
        {
            Itinerary itinerary = new Itinerary() { FirtsStopAddress = firstStopAddress, SecondStopAddress = secondStopAddress, ThirdStopAddress = thirdStopAddress };
            _bddContext.Itineraries.Add(itinerary);
            _bddContext.SaveChanges();
            return itinerary.Id;
        }
        public void CreateItinerary(Itinerary itinerary)
        {
            _bddContext.Itineraries.Update(itinerary);
            _bddContext.SaveChanges();
        }
        public void UpdateItinerary(int id, string firstStopAddress, string secondStopAddress, string thirdStopAddress)
        {
            Itinerary itinerary = _bddContext.Itineraries.Find(id);

            if (itinerary != null)
            {
                itinerary.Id = id;
                itinerary.FirtsStopAddress = firstStopAddress;
                itinerary.SecondStopAddress = secondStopAddress;
                itinerary.ThirdStopAddress = thirdStopAddress;
                _bddContext.SaveChanges();
            }

        }
        public void UpdateItinerary(Itinerary itinerary)
        {
            _bddContext.Itineraries.Update(itinerary);
            _bddContext.SaveChanges();
        }

        public void DeleteItinerary(int id)
        {
            Itinerary itinerary = _bddContext.Itineraries.Find(id);
            if (itinerary != null)
            {
                _bddContext.Itineraries.Remove(itinerary);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------


        #region CRUD Trajectory

        public List<Trajectory> GetAllTrajectories()
        {
            return _bddContext.Trajectories.Include(e => e.Itinerary).ToList();
        }

        public Trajectory GetTrajectory(int id)
        {
            return _bddContext.Trajectories.Include(e => e.Itinerary).FirstOrDefault(e => e.Id == id);
        }
        public int CreateTrajectory(int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress, TrajectoryType selectTrajectoryType, int itineraryId)
        {
            Trajectory trajectory = new Trajectory()
            {
                DurationHours = durationHours,
                StopNumber = stopNumber,
                StopsDurationMinutes = stopsDurationMinutes,
                PickUpAddress = pickUpAddress,
                DeliveryAddress = deliveryAddress,
                SelectTrajectoryType = selectTrajectoryType,
                Itinerary = _bddContext.Itineraries.First(b => b.Id == itineraryId)
            };
            _bddContext.Trajectories.Add(trajectory);
            _bddContext.SaveChanges();
            return trajectory.Id;
        }
        public void CreateTrajectories(Trajectory trajectory)
        {
            _bddContext.Trajectories.Update(trajectory);
            _bddContext.SaveChanges();
        }
        public void UpdateTrajectory(int id, int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress, TrajectoryType selectTrajectoryType, int itineraryId)
        {
            Trajectory trajectory = _bddContext.Trajectories.Find(id);

            if (trajectory != null)
            {
                trajectory.Id = id;
                trajectory.DurationHours = durationHours;
                trajectory.StopNumber = stopNumber;
                trajectory.StopsDurationMinutes = stopsDurationMinutes;
                trajectory.PickUpAddress = pickUpAddress;
                trajectory.DeliveryAddress = deliveryAddress;
                trajectory.SelectTrajectoryType = selectTrajectoryType;
                trajectory.Itinerary = _bddContext.Itineraries.First(b => b.Id == itineraryId);
                _bddContext.SaveChanges();
            }

        }
        public void UpdateTrajectory(Trajectory trajectory)
        {
            _bddContext.Trajectories.Update(trajectory);
            _bddContext.SaveChanges();
        }

        public void DeleteTrajectory(int id)
        {
            Trajectory trajectory = _bddContext.Trajectories.Find(id);
            if (trajectory != null)
            {
                _bddContext.Trajectories.Remove(trajectory);
                _bddContext.SaveChanges();
            }
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Reservation

        public List<Reservation> GetAllReservations()
        {
            return _bddContext.Reservations.Include(r => r.ServiceConsumed).Include(r => r.ServiceUserConsumer).ToList();
        }

        //Create Reservation
        public Reservation CreateReservation(int serviceConsumedId, int serviceUserConsumerId)
        {
            Reservation reservation = new Reservation()
            {

                ServiceConsumed = _bddContext.Services.First(s => s.Id == serviceConsumedId),
                ServiceUserConsumer = _bddContext.Users.First(s => s.Id == serviceUserConsumerId)
            };
            _bddContext.Reservations.Add(reservation);
            _bddContext.SaveChanges();
            return reservation;
        }
        public void CreateReservation(Reservation reservation)
        {
            _bddContext.Reservations.Update(reservation);
            _bddContext.SaveChanges();
        }

        //Update Reservation
        public void UpdateReservation(int id, int serviceConsumedId, int serviceUserConsumerId)
        {
            Reservation reservation = _bddContext.Reservations.Find(id);

            if (reservation != null)
            {
                reservation.Id = id;
                reservation.ServiceConsumed = _bddContext.Services.First(s => s.Id == serviceConsumedId);
                reservation.ServiceUserConsumer = _bddContext.Users.First(s => s.Id == serviceUserConsumerId);
                _bddContext.SaveChanges();
            }
        }

        public void UpdateReservation(Reservation reservation)
        {
            _bddContext.Reservations.Update(reservation);
            _bddContext.SaveChanges();
        }

        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD ServiceRequestFinal

        public int CreateServiceRequest(DateTime publicationDate, DateTime expirationDate, int referenceNumber, bool isAvailable, DateTime start, DateTime end, bool isRequest, ServiceType selectServiceType, int? userProviderId)
        {
            Service serviceRequest = new Service()
            {
                PublicationDate = publicationDate,
                ExpirationDate = expirationDate,
                ReferenceNumber = referenceNumber,
                IsAvailable = true,
                Start = start,
                End = end,
                IsRequest = true,
                SelectServiceType = selectServiceType,
                UserProvider = _bddContext.Users.First(s => s.Id == userProviderId)
            };
            _bddContext.Services.Add(serviceRequest);
            _bddContext.SaveChanges();
            return serviceRequest.Id;
        }
        public void CreateServiceRequestFinal(Service serviceRequest)
        {
            _bddContext.Services.Update(serviceRequest);
            _bddContext.SaveChanges();
        }
        #endregion

        //-------------------------------------------------------------------------------------------------
    }

}