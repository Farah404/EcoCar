using EcoCar.Models.DataBase;
using EcoCar.Models.PersonManagement;
using System.Collections.Generic;
using System.Linq;

namespace EcoCar.Models.Services
{
    public class DalAccount : IDalAccount
    {
        private BddContext _bddContext;
        public DalAccount()
        {
            _bddContext = new BddContext();
        }

        public List<Account> GetAllAccounts()
        {
            return _bddContext.Accounts.ToList();
        }

        public int CreateAccount(string username, string password, bool isActive)
        {
            Account account = new Account() { Username = username, Password = password, IsActive = isActive };
            _bddContext.Accounts.Add(account);
            _bddContext.SaveChanges();
            return account.Id;
        }
        public void CreateAccount(Account account)
        {
            _bddContext.Accounts.Update(account);
            _bddContext.SaveChanges();
        }

        public void UpdateAccount(int id, string username, string password, bool isActive)
        {
            Account account = _bddContext.Accounts.Find(id);

            if (account != null)
            {
                account.Id = id;
                account.Username = username;
                account.Password = password;
                account.IsActive = isActive;
                _bddContext.SaveChanges();
            }

        }
        public void UpdateAccount(Account account)
        {
            _bddContext.Accounts.Update(account);
            _bddContext.SaveChanges();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
