using EcoCar.Models.FinancialManagement;
using EcoCar.Models.MessagingManagement;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.ServiceManagement;
using System.Collections.Generic;

namespace EcoCar.ViewModels
{
    public class AccountViewModel
    {
        public Person person { get; set; }

        public Account Account { get; set; }

        public bool Authentification { get; set; }

        public Person Person { get; set; }

        public User User { get; set; }

        public Administrator Administrator { get; set; }

        public Vehicule Vehicule { get; set; }

        public Insurance Insurance { get; set; }

        public EcoWallet EcoWallet { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        //public Reporting Reporting { get; set; }

        //public UserReporting UserReporting { get; set; }

        //public HelpReporting HelpReporting { get; set; }

        public Message Message { get; set; }

        public List<User> Users { get; set; }

        public List<Service> Services { get; set; }

        public List<CarPoolingService> CarPoolingServices { get; set; }

        public List<CarRentalService> CarRentalServices { get; set; }

        public List<ParcelService> ParcelServices { get; set; }

        public List<Account> Accounts { get; set; }

        public List<EcoStoreInvoice> EcoStoreInvoices { get; set; }

        public List<ServiceInvoice> ServiceInvoices { get; set; }
        public List<Message> Messages { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}