using EcoCar.Models.DataBase;
using EcoCar.Models.PersonManagement;
using EcoCar.Models.FinancialManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSystem.Security.Cryptography;
using static EcoCar.Models.PersonManagement.AccountUser;

namespace EcoCar.Models.Services
{
    public class DalPersonManagement : IDalPersonManagement
    {
        private BddContext _bddContext;
        public DalPersonManagement()
        {
            _bddContext = new BddContext();
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Person

        public List<Person> GetAllPeople()
        {
            return _bddContext.People.ToList();
        }

        //Create Person
        public int CreatePerson(string name, string lastName, string profilePictureURL)
        {
            Person person = new Person() { Name = name, LastName = lastName, ProfilePictureURL = profilePictureURL };
            _bddContext.People.Add(person);
            _bddContext.SaveChanges();
            return person.Id;
        }

        public void CreatePerson(Person person)
        {
            _bddContext.People.Update(person);
            _bddContext.SaveChanges();
        }

        //Update Person
        public void UpdatePerson(int id, string name, string lastName, string profilePictureURL)
        {
            Person person = _bddContext.People.Find(id);

            if (person != null)
            {
                person.Id = id;
                person.Name = name;
                person.LastName = lastName;
                person.ProfilePictureURL = profilePictureURL;
                _bddContext.SaveChanges();
            }
        }
        public void UpdatePerson(Person person)
        {
            _bddContext.People.Update(person);
            _bddContext.SaveChanges();
        }

        //Delete Person
        public void DeletePerson(int id)
        {
            Person person = _bddContext.People.Find(id);

            if (person != null)
            {
                _bddContext.People.Remove(person);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD User
        public List<User> GetAllUsers()
        {
            List<User> listUsers = _bddContext.Users.Include(e => e.BankDetails).Include(e => e.BillingAddress).Include(e => e.Person).ToList();
            return listUsers;
        }

        public User GetUser(int id)
        {
            return _bddContext.Users.Include(e => e.BankDetails).Include(e => e.BillingAddress).Include(e => e.Person).FirstOrDefault(e => e.Id == id);
        }


        //Create User
        public User CreateUser(string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, int bankDetailsId, int billingAddressId, int personId, int? vehiculeId)
        {
            User user = new User() { 
                Email = email, 
                BirthDate = birthDate, 
                PhoneNumber = phoneNumber, 
                IdentityCardNumber = identityCardNumber, 
                DrivingPermitNumber = drivingPermitNumber,
                BankDetails = _bddContext.BankingDetails.First(b => b.Id == bankDetailsId),
                BillingAddress = _bddContext.BillingAddresses.First(b => b.Id == billingAddressId),
                Person = _bddContext.People.First(b => b.Id == personId),
                Vehicule = _bddContext.Vehicules.FirstOrDefault(b => b.Id == vehiculeId)
            };
            _bddContext.Users.Add(user);
            _bddContext.SaveChanges();
            return user;
        }
        public void CreateUser(User user)
        {
            _bddContext.Users.Update(user);
            _bddContext.SaveChanges();
        }

        //Update User
        public void UpdateUser(
            int id,
            string email,
            DateTime birthDate,
            int phoneNumber,
            int identityCardNumber,
            int drivingPermitNumber,
            int bankDetailsId,
            int billingAddressId,
            int personId
            //string name,
            //string lastName,
            //string profilePictureURL

            )
        {
            
            User userToUpdate = _bddContext.Users.Find(id);

            if (userToUpdate != null)
            {
                userToUpdate.Id = id;
                userToUpdate.Email = email;
                userToUpdate.BirthDate = birthDate;
                userToUpdate.PhoneNumber = phoneNumber;
                userToUpdate.IdentityCardNumber = identityCardNumber;
                userToUpdate.BankDetails = _bddContext.BankingDetails.First(b => b.Id == bankDetailsId);
                userToUpdate.BillingAddress = _bddContext.BillingAddresses.First(b => b.Id == billingAddressId);
                userToUpdate.Person = _bddContext.People.First(b => b.Id == personId);
                //userToUpdate.Person.Name = name;
                //userToUpdate.Person.LastName = lastName;
                //userToUpdate.Person.ProfilePictureURL = profilePictureURL;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateUser(User user)
        {
            _bddContext.Users.Update(user);
            _bddContext.SaveChanges();
        }

        //Delete User
        public void DeleteUser(int id)
        {
            User user = _bddContext.Users.Find(id);

            if (user != null)
            {
                _bddContext.Users.Remove(user);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Administrator
        public List<Administrator> GetAllAdministrators()
        {
            return _bddContext.Administrators.Include(e => e.Person).ToList();
        }

        public Administrator GetAdministrator(int id)
        {
            return _bddContext.Administrators.Include(e => e.Person).FirstOrDefault(e => e.Id == id);
        }

        //Create Administrator
        public int CreateAdministrator(string emailPro, int phoneNumberPro, int personId)
        {
            Administrator administrator = new Administrator() {
                EmailPro = emailPro, 
                PhoneNumberPro = phoneNumberPro,
                Person = _bddContext.People.First(b => b.Id == personId)
            };
            _bddContext.Administrators.Add(administrator);
            _bddContext.SaveChanges();
            return administrator.Id;
        }
        public void CreateAdministrator(Administrator administrator)
        {
            _bddContext.Administrators.Update(administrator);
            _bddContext.SaveChanges();
        }

        //Update Administrator
        public void UpdateAdministrator(int id, string emailPro, int phoneNumberPro, int personId)
        {
            Administrator administrator = _bddContext.Administrators.Find(id);

            if (administrator != null)
            {
                administrator.Id = id;
                administrator.EmailPro = emailPro;
                administrator.PhoneNumberPro = phoneNumberPro;
                administrator.Person = _bddContext.People.First(b => b.Id == personId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAdministrator(Administrator administrator)
        {
            _bddContext.Administrators.Update(administrator);
            _bddContext.SaveChanges();
        }

        //Delete Administrator
        public void DeleteAdministrator(int id)
        {
            Administrator administrator = _bddContext.Administrators.Find(id);

            if (administrator != null)
            {
                _bddContext.Administrators.Remove(administrator);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Account
        public List<Account> GetAllAccounts()
        {
            return _bddContext.Accounts.Include(e => e.Person).ToList(); 
        }

        public Account GetAccount(int id)
        {
            return this._bddContext.Accounts.Include(e => e.Person).FirstOrDefault(e => e.Id == id);
        }

        public Account GetAccount(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAccount(id);
            }
            return null;
        }
        public static string EncodeMD5(string password)
        {
            string passwordOne = "EcoCar" + password + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(passwordOne)));
        }


        //Create Account
        public int CreateAccount(string username, string passwordClear, bool isActive, int personId)
        {
            string password = EncodeMD5(passwordClear);
            Account account = new Account() { 
                Username = username, 
                Password = password, 
                IsActive = isActive, 
                Person = _bddContext.People.First(b=>b.Id == personId) 
            };
            _bddContext.Accounts.Add(account);
            _bddContext.SaveChanges();
            return account.Id;
        }
        public void CreateAccount(Account account)
        {
            _bddContext.Accounts.Update(account);
            _bddContext.SaveChanges();
        }

        //Update Account
        public void UpdateAccount(int id, string username, string password, bool isActive, int personId)
        {
            Account account = _bddContext.Accounts.Find(id);

            if (account != null)
            {
                account.Id = id;
                account.Username = username;
                account.Password = password;
                account.IsActive = isActive;
                account.Person = _bddContext.People.First(b => b.Id == personId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAccount(Account account)
        {
            _bddContext.Accounts.Update(account);
            _bddContext.SaveChanges();
        }

        //Delete Account
        public void DeleteAccount(int id)
        {
            Account account = _bddContext.Accounts.Find(id);

            if (account != null)
            {
                _bddContext.Accounts.Remove(account);
                _bddContext.SaveChanges();
            }
        }

        public Account Authentify(string username, string passwordClear)
        {
            string password = EncodeMD5(passwordClear);
            Account account = this._bddContext.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
            return account;
        }


        //-------------------------------------------------------------------------------------------------

        //CRUD AccountUser
        public List<AccountUser> GetAllAccountUsers()
        {
            return _bddContext.AccountUsers.Include(e => e.EcoWallet).Include(e => e.Vehicule).Include(e => e.Account).ToList();
        }

        public AccountUser GetAccountUser(int id)
        {
            return _bddContext.AccountUsers.Include(e => e.EcoWallet).Include(e => e.Vehicule).Include(e => e.Account).FirstOrDefault(e => e.Id == id);
        }

        //Create AccountUser
        public int CreateAccountUser(double userRating, EcoStatusType selectEcoStatusType, int ecoWalletId, int vehiculeId, int accountId)
        {
            AccountUser accountUser = new AccountUser() { 
                UserRating = userRating,
                SelectEcoStatusType = selectEcoStatusType,
                EcoWallet = _bddContext.EcoWallets.First(b => b.Id == ecoWalletId),
                Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId),
                Account = _bddContext.Accounts.First(b => b.Id == accountId),
            };
            _bddContext.AccountUsers.Add(accountUser);
            _bddContext.SaveChanges();
            return accountUser.Id;
        }
        public void CreateAccountUser(AccountUser accountUser)
        {
            _bddContext.AccountUsers.Update(accountUser);
            _bddContext.SaveChanges();
        }

        //Update AccountUser
        public void UpdateAccountUser(int id, double userRating, EcoStatusType selectEcoStatusType, int ecoWalletId, int vehiculeId, int accountId)
        {
            AccountUser accountUser = _bddContext.AccountUsers.Find(id);

            if (accountUser != null)
            {
                accountUser.Id = id;
                accountUser.UserRating = userRating;
                accountUser.SelectEcoStatusType = selectEcoStatusType;
                accountUser.EcoWallet = _bddContext.EcoWallets.First(b => b.Id == ecoWalletId);
                accountUser.Vehicule = _bddContext.Vehicules.First(b => b.Id == vehiculeId);
                accountUser.Account = _bddContext.Accounts.First(b => b.Id == accountId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAccountUser(AccountUser accountUser)
        {
            _bddContext.AccountUsers.Update(accountUser);
            _bddContext.SaveChanges();
        }

        //Delete AccountUser
        public void DeleteAccountUser(int id)
        {
            AccountUser accountUser = _bddContext.AccountUsers.Find(id);

            if (accountUser != null)
            {
                _bddContext.AccountUsers.Remove(accountUser);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD AccountAdministrator
        public List<AccountAdministrator> GetAllAccountAdministrators()
        {
            return _bddContext.AccountAdministrators.Include(e => e.Account).ToList();
        }

        public AccountAdministrator GetAccountAdministrators(int id)
        {
            return _bddContext.AccountAdministrators.Include(e => e.Account).FirstOrDefault(e => e.Id == id);
        }

        //Create AccountAdministrator
        public int CreateAccountAdministrator(string employeeCode, int accountId)
        {
            AccountAdministrator accountAdministrator = new AccountAdministrator() {
                EmployeeCode = employeeCode,
                Account = _bddContext.Accounts.First(b => b.Id == accountId)
            };
            _bddContext.AccountAdministrators.Add(accountAdministrator);
            _bddContext.SaveChanges();
            return accountAdministrator.Id;
        }
        public void CreateAccountAdministrator(AccountAdministrator accountAdministrator)
        {
            _bddContext.AccountAdministrators.Update(accountAdministrator);
            _bddContext.SaveChanges();
        }

        //Update AccountAdministrator
        public void UpdateAccountAdministrator(int id, string employeeCode, int accountId)
        {
            AccountAdministrator accountAdministrator = _bddContext.AccountAdministrators.Find(id);

            if (accountAdministrator != null)
            {
                accountAdministrator.Id = id;
                accountAdministrator.EmployeeCode = employeeCode;
                accountAdministrator.Account = _bddContext.Accounts.First(b => b.Id == accountId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAccountAdministrator(AccountAdministrator accountAdministrator)
        {
            _bddContext.AccountAdministrators.Update(accountAdministrator);
            _bddContext.SaveChanges();
        }

        //Delete AccountAdministrator
        public void DeleteAccountAdministrator(int id)
        {
            AccountAdministrator accountAdministrator = _bddContext.AccountAdministrators.Find(id);

            if (accountAdministrator != null)
            {
                _bddContext.AccountAdministrators.Remove(accountAdministrator);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Vehicule
        public List<Vehicule> GetAllVehicules()
        {
            return _bddContext.Vehicules.Include(e => e.Insurance).ToList();
        }

        public Vehicule GetVehicules(int id)
        {
            return _bddContext.Vehicules.Include(e => e.Insurance).FirstOrDefault(e => e.Id == id);
        }

        //Create Vehicule
        public Vehicule CreateVehicule(string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId)
        {
            Vehicule vehicule = new Vehicule() {
                Brand = brand, 
                RegistrationNumber = registrationNumber, 
                Model = model, 
                Hybrid = hybrid, 
                Electric = electric, 
                TechnicalTestExpiration = technicalTestExpiration, 
                AvailableSeats = availableSeats,
                Insurance = _bddContext.Insurances.First(b => b.Id == insuranceId)
            };
            _bddContext.Vehicules.Add(vehicule);
            _bddContext.SaveChanges();
            return vehicule;
        }
        public void CreateVehicule(Vehicule vehicule)
        {
            _bddContext.Vehicules.Update(vehicule);
            _bddContext.SaveChanges();
        }

        //Update Vehicule
        public void UpdateVehicule(int id, string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId)
        {
            Vehicule vehicule = _bddContext.Vehicules.Find(id);

            if (vehicule != null)
            {
                vehicule.Id = id;
                vehicule.Brand = brand;
                vehicule.RegistrationNumber = registrationNumber;
                vehicule.Model = model;
                vehicule.Hybrid = hybrid;
                vehicule.Electric = electric;
                vehicule.TechnicalTestExpiration = technicalTestExpiration;
                vehicule.AvailableSeats = availableSeats;
                vehicule.Insurance = _bddContext.Insurances.First(b => b.Id == insuranceId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateVehicule(Vehicule vehicule)
        {
            _bddContext.Vehicules.Update(vehicule);
            _bddContext.SaveChanges();
        }

        //Delete Vehicule
        public void DeleteVehicule(int id)
        {
            Vehicule vehicule = _bddContext.Vehicules.Find(id);

            if (vehicule != null)
            {
                _bddContext.Vehicules.Remove(vehicule);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        //CRUD Insurance
        public List<Insurance> GetAllInsurances()
        {
            return _bddContext.Insurances.ToList();
        }

        //Create Insurance
        public int CreateInsurance(string insuranceAgency, DateTime insuranceExpiration, string contractNumber)
        {
            Insurance insurance = new Insurance() { InsuranceAgency = insuranceAgency, InsuranceExpiration = insuranceExpiration, ContractNumber = contractNumber };
            _bddContext.Insurances.Add(insurance);
            _bddContext.SaveChanges();
            return insurance.Id;
        }
        public void CreateInsurance(Insurance insurance)
        {
            _bddContext.Insurances.Update(insurance);
            _bddContext.SaveChanges();
        }

        //Update Insurance
        public void UpdateInsurance(int id, string insuranceAgency, DateTime insuranceExpiration, string contractNumber)
        {
            Insurance insurance = _bddContext.Insurances.Find(id);

            if (insurance != null)
            {
                insurance.Id = id;
                insurance.InsuranceAgency = insuranceAgency;
                insurance.InsuranceExpiration = insuranceExpiration;
                insurance.ContractNumber = contractNumber;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateInsurance(Insurance insurance)
        {
            _bddContext.Insurances.Update(insurance);
            _bddContext.SaveChanges();
        }

        //Delete Insurance
        public void DeleteInsurance(int id)
        {
            Insurance insurance = _bddContext.Insurances.Find(id);

            if (insurance != null)
            {
                _bddContext.Insurances.Remove(insurance);
                _bddContext.SaveChanges();
            }
        }

        //-------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _bddContext.Dispose();
        }

    }
}
