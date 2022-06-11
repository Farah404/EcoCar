namespace EcoCar.Models.ServiceManagement
{
    public class ServiceTypeLink
    {
        //Primary Key
        public int Id { get; set; }

        //Foreign Keys
        public int? CarPoolingServiceId { get; set; }
        public CarPoolingService CarPoolingService { get; set; }
        public int? ParcelServiceId { get; set; }
        public ParcelService ParcelService { get; set; }    
        public int? CarRentalServiceId { get; set; }
        public CarRentalService CarRentalService { get; set; }

    }
}
