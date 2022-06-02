using EcoCar.Models.DataBase;
using EcoCar.Models.ServiceManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcoCar.Models.Services
{
    public class DalServiceManagement
    {
        private BddContext _bddContext;
        public DalServiceManagement()
        {
            _bddContext = new BddContext();
        }

        //-------------------------------------------------------------------------------------------------

        // CRUD Service 

        public List<Service> GetAllServices()
        {
            return _bddContext.Services.ToList();
        }
        public int CreateService(DateTime publicationDateTime, DateTime expirationDateTime, int referenceNumber, bool isExpired, DateTime start, DateTime end, int serviceTypeId)
        {
            Service service = new Service() { PublicationDateTime = publicationDateTime, ExpirationDateTime = expirationDateTime, ReferenceNumber = referenceNumber, IsExpired = isExpired, Start = start, End = end, ServiceTypeId = serviceTypeId };
            _bddContext.Services.Add(service);
            _bddContext.SaveChanges();
            return service.Id;
        }
        public void CreateService(Service service)
        {
            _bddContext.Services.Update(service);
            _bddContext.SaveChanges();
        }
        public void UpdateService(int id, DateTime publicationDateTime, DateTime expirationDateTime, int referenceNumber, bool isExpired, DateTime start, DateTime end, int serviceTypeId)
        {
            Service service = _bddContext.Services.Find(id);

            if (service != null)
            {
                service.Id = id;
                service.PublicationDateTime = publicationDateTime;
                service.ExpirationDateTime = expirationDateTime;
                service.ReferenceNumber = referenceNumber;
                service.IsExpired = isExpired;
                service.Start = start;
                service.End = end;
                service.ServiceTypeId = serviceTypeId;
                _bddContext.SaveChanges();
            }

        }
        public void UpdateService(Service service)
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

        //-------------------------------------------------------------------------------------------------

        //CRUD CarPoolingService

        public List<CarPoolingService> GetAllCarPoolingServices()
        {
            return _bddContext.CarPoolingServices.ToList();
        }
        public int CreateCarPoolingService(int carPoolingTypeId, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed)
        {
            CarPoolingService carPoolingService = new CarPoolingService() { CarPoolingTypeId = carPoolingTypeId, AvailableSeats = avalaibleSeats, PetsAllowed = petsAllowed, SmokingAllowed = smokingAllowed, MusicAllowed = musicAllowed, ChattingAllowed = chattingAllowed };
            _bddContext.CarPoolingServices.Add(carPoolingService);
            _bddContext.SaveChanges();
            return carPoolingService.Id;
        }
        public void CreateCarPoolingService(CarPoolingService service)
        {
            _bddContext.CarPoolingServices.Update(service);
            _bddContext.SaveChanges();
        }
        public void UpdateCarPoolingService(int id, int carPoolingTypeId, int avalaibleSeats, bool petsAllowed, bool smokingAllowed, bool musicAllowed, bool chattingAllowed)
        {
            CarPoolingService service = _bddContext.CarPoolingServices.Find(id);

            if (service != null)
            {
                service.Id = id;
                service.CarPoolingTypeId = carPoolingTypeId;
                service.AvailableSeats = avalaibleSeats;
                service.PetsAllowed = petsAllowed;
                service.SmokingAllowed = smokingAllowed;
                service.MusicAllowed = musicAllowed;
                service.ChattingAllowed = chattingAllowed;
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
        //-------------------------------------------------------------------------------------------------

        //CRUD CarRentalService

        public List<CarRentalService> GetAllCarRentalServices()
        {
            return _bddContext.CarRentalServices.ToList();
        }
        public int CreateCarRentalService(string keyPickUpAdress, string keyDropOffAdress)
        {
            CarRentalService carRentalService = new CarRentalService() { KeyPickUpAddress = keyPickUpAdress, KeyDropOffAddress = keyDropOffAdress };
            _bddContext.CarRentalServices.Add(carRentalService);
            _bddContext.SaveChanges();
            return carRentalService.Id;
        }
        public void CreateCarRentalService(CarRentalService carRentalService)
        {
            _bddContext.CarRentalServices.Update(carRentalService);
            _bddContext.SaveChanges();
        }
        public void UpdateCarRentalService(int id, string keyPickUpAdress, string keyDropOffAdress)
        {
            CarRentalService carRentalService = _bddContext.CarRentalServices.Find(id);

            if (carRentalService != null)
            {
                carRentalService.Id = id;
                carRentalService.KeyPickUpAddress = keyPickUpAdress;
                carRentalService.KeyDropOffAddress = keyDropOffAdress;
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

        //-------------------------------------------------------------------------------------------------

        //CRUD Itinerary

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
        public void UpdateItineray(int id, string firstStopAddress, string secondStopAddress, string thirdStopAddress)
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
        //-------------------------------------------------------------------------------------------------

        //CRUD ParcelService

        public List<ParcelService> GetAllParcelServices()
        {
            return _bddContext.ParcelServices.ToList();
        }
        public int CreateParcelService(int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile)
        {
            ParcelService parcelService = new ParcelService() { BarCode = barCode, WeightKilogrammes = weightKilogrammes, AtypicalVolume = atypicalVolume, Fragile = fragile };
            _bddContext.ParcelServices.Add(parcelService);
            _bddContext.SaveChanges();
            return parcelService.Id;
        }
        public void CreateParcelService(ParcelService parcelService)
        {
            _bddContext.ParcelServices.Update(parcelService);
            _bddContext.SaveChanges();
        }
        public void UpdateParcelService(int id, int barCode, double weightKilogrammes, bool atypicalVolume, bool fragile)
        {
            ParcelService parcelService = _bddContext.ParcelServices.Find(id);

            if (parcelService != null)
            {
                parcelService.Id = id;
                parcelService.BarCode = barCode;
                parcelService.WeightKilogrammes = weightKilogrammes;
                parcelService.AtypicalVolume = atypicalVolume;
                parcelService.Fragile = fragile;
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

        //-------------------------------------------------------------------------------------------------

        //CRUD Trajectory

        public List<Trajectory> GetAllTrajectories()
        {
            return _bddContext.Trajectories.ToList();
        }
        public int CreateTrajectory(int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress)
        {
            Trajectory trajectory = new Trajectory() { DurationHours = durationHours, StopNumber = stopNumber, StopsDurationMinutes = stopsDurationMinutes, PickUpAddress = pickUpAddress, DeliveryAddress = deliveryAddress };
            _bddContext.Trajectories.Add(trajectory);
            _bddContext.SaveChanges();
            return trajectory.Id;
        }
        public void CreateTrajectories(Trajectory trajectory)
        {
            _bddContext.Trajectories.Update(trajectory);
            _bddContext.SaveChanges();
        }
        public void UpdateTrajectory(int id, int durationHours, int stopNumber, int stopsDurationMinutes, string pickUpAddress, string deliveryAddress)
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
                _bddContext.SaveChanges();
            }

        }
        public void UpdateTrajectory(Trajectory trajectory)
        {
            _bddContext.Trajectories.Update(trajectory);
            _bddContext.SaveChanges();
        }

        public void DeleteTrajectories(int id)
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
    }

}
