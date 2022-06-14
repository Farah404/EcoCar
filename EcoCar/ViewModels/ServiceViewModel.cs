using EcoCar.Models.PersonManagement;
using EcoCar.Models.ServiceManagement;
using System.Collections.Generic;

namespace EcoCar.ViewModels
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }

        public CarPoolingService CarPoolingService { get; set; }

        public CarRentalService CarRentalService { get; set; }

        public ParcelService ParcelService { get; set; }

        public Trajectory Trajectory { get; set; }

        public List <CarPoolingService> CarPoolingServices { get; set; }

        public List <ParcelService> ParcelServices { get; set; }

        public List <CarRentalService> CarRentalServices { get; set; }

        public List<Service> Services { get; set; }    

        public User User { get; set; }
        public Account Account { get; set; }
        public List<Account>Accounts { get; set; }
        public List<User> Users { get; set; }


    }
}
