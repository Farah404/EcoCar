using EcoCar.Models.ServiceManagement;

namespace EcoCar.ViewModels
{
    public class ServiceViewModel
    {
        public Service Service { get; set; }

        public CarPoolingService CarPoolingService { get; set; }

        public CarRentalService CarRentalService { get; set; }

        public ParcelService ParcelService { get; set; }

        public Trajectory trajectory { get; set; }

    }
}
