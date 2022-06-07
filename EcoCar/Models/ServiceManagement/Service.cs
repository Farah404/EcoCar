//Class Description
//The Service.cs contains all details related to a service that can be purchased or proposed by a user

using System;
using System.ComponentModel.DataAnnotations;

namespace EcoCar.Models.ServiceManagement
{
    public class Service
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int IdServiceProvider { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ReferenceNumber { get; set; }
        public bool IsExpired { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [Required]
        public virtual int ServiceTypeId
        {
            get
            {
                return (int)this.Services;
            }
            set
            {
                Services = (ServiceType)value;
            }
        }
        [EnumDataType(typeof(ServiceType))]
        public ServiceType Services { get; set; }
        public enum ServiceType
        {
            CarPoolingService = 0,
            ParcelService = 1,
            CarRentalService = 2
        }

        //Foreign Keys

    }
}
