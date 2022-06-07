using EcoCar.Models.PersonManagement;

namespace EcoCar.ViewModels
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public bool Authentification { get; set; }
        public Person Person { get; set; }
        public User User { get; set; }

    }
}
