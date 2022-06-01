using EcoCar.Models.PersonManagement;
using System;
using System.Collections.Generic;

namespace EcoCar.Models.Services
{
    public interface IDalAccount : IDisposable
    {
        List<Account> GetAllAccounts();

        int CreateAccount(string username, string password, bool isActive);
        void UpdateAccount(int id, string username, string password, bool isActive);
    }
}
