//Class Description
//The ServiceInvoice.cs contains the billing details of a purchased service

using EcoCar.Models.ServiceManagement;

namespace EcoCar.Models.FinancialManagement
{
    public class ServiceInvoice
    {
        //Primary Key
        public int Id { get; set; }

        //Attributes
        public int IdServiceProvider { get; set; }
        public int IdServiceConsumer { get; set; }
        public int EcoCoinAmount { get; set; }

        //Foreign Keys
        public int ServiceId { get; set; }
        public Service Service { get; set; }

        //Inheritance Class
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}