using EcoCar.Models.ServiceManagement;
using System.Collections.Generic;

namespace EcoCar.ViewModels
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }

        public ServiceRequest ServiceRequest { get; set; }

        public CarPoolingService CarPoolingService { get; set; }

        public CarRentalService CarRentalService { get; set; }

        public ParcelService ParcelService { get; set; }

        public Trajectory trajectory { get; set; }

        public List <CarPoolingService> CarPoolingServices { get; set; }

        public List <ParcelService> ParcelServices { get; set; }

        public List <CarRentalService> CarRentalServices { get; set; }
    }
}
