using EcoCar.Models.PersonManagement;
using System;

namespace EcoCar.Models.ServiceManagement
{
    public class ServiceRequest
    {
        public int Id { get; set; }

        public DateTime PublicationDate { get; set; }
        public int ReferenceNumber { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        //Foreign Keys
        public int? UserProviderId { get; set; }
        public User UserProvider { get; set; }

    }
}
