using EcoCar.Models.PersonManagement;
using System;

namespace EcoCar.Models.ServiceManagement
{
    public class ServiceRequestFinal
    {
        //General
        public int Id { get; set; }
        public DateTime PublicationDate { get; set; }
        public int ReferenceNumber { get; set; }
        public DateTime Start { get; set; }
        public ServiceRequestType SelectServiceRequestType { get; set; }
        public enum ServiceRequestType
        {
            CarPoolingService,
            ParcelService,
            CarRentalService
        }
        public string PickUpAddress { get; set; }
        public string DeliveryAddress { get; set; }

        //CarPoolingServiceRequest
        public CarPoolingRequestType SelectCarPoolingRequestType { get; set; }
        public enum CarPoolingRequestType
        {
            HomeToWork,
            HomeToSchool,
            Events,
            Travel
        }
        public int PassengerNumber { get; set; }
        public int PetsNumber { get; set; }
        public bool Smoking { get; set; }
        public bool Music { get; set; }
        public bool Chatting { get; set; }

        //ParcelRequestServiceRequest
        public int BarCode { get; set; }
        public double WeightKilogrammes { get; set; }
        public bool AtypicalVolume { get; set; }
        public bool Fragile { get; set; }

        //CarRentalServicerequest
        public string KeyPickUpAddress { get; set; }
        public string KeyDropOffAddress { get; set; }
        public string UsageComments { get; set; }

        //Inheritance Class
        public int? UserProviderId { get; set; }
        public User UserProvider { get; set; }


    }
}
