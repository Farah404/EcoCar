//Class Description
//The Service.cs contains all details related to a service that can be purchased or proposed by a user

using EcoCar.Models.PersonManagement;
using System;
using System.Collections.Generic;

namespace EcoCar.Models.ServiceManagement
{
    public class Service
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public DateTime PublicationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ReferenceNumber { get; set; }

        public bool IsAvailable { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool IsRequest { get; set; }
        public ServiceType SelectServiceType { get; set; }
        public enum ServiceType
        {
            CarPoolingService,
            ParcelService,
            CarRentalService
        }
        public int PriceEcoCoins { get; set; }
        //Foreign Keys
        public int UserProviderId { get; set; }
        public User UserProvider { get; set; }

        public Service()
        {
            this.CarPoolingServices = new HashSet<CarPoolingService>();
            this.ParcelServices = new HashSet<ParcelService>();
            this.CarRentalServices = new HashSet<CarRentalService>();

        }
        public virtual ICollection<CarPoolingService> CarPoolingServices { get; set; }
        public virtual ICollection<ParcelService> ParcelServices { get; set; }
        public virtual ICollection<CarRentalService> CarRentalServices { get; set; }
    }

}

