using EcoCar.Models.PersonManagement;
using EcoCar.Models.Services;
using System;

namespace EcoCar.Models.Services
{
    public interface IDalUser : IDisposable
    {

        //User AjouterUser(string nom, string password);
        //User AjouterUser(User User);
        User AjouterUser(string nom, string password, Role role = Role.ReadWrite);
        User Authentifier(string nom, string password);
        User ObtenirUser(int id);
        User ObtenirUser(string idStr);

    }
}
