using EcoCar.Models.PersonManagement;

namespace EcoCar.ViewModels
{
    public class AccountViewModel
    {
        public Person person { get; set; }
        public Account Account { get; set; }
        public bool Authentification { get; set; }
        public Person Person { get; set; }
        public User User { get; set; }
        public Vehicule Vehicule { get; set; }
        public Insurance Insurance { get; set; }


    }
}
