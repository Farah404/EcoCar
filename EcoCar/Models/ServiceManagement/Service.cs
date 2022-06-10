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
        public bool IsExpired { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ServiceType SelectServiceType { get; set; }
        public enum ServiceType
        {
            CarPoolingService,
            ParcelService,
            CarRentalService
        }
        public int? UserProviderId { get; set; }
        public User UserProvider { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }

    }

}


