﻿using EcoCar.Models.DataBase;
using EcoCar.Models.PersonManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XSystem.Security.Cryptography;
using static EcoCar.Models.PersonManagement.User;

namespace EcoCar.Models.Services
{
    public class DalPersonManagement : IDalPersonManagement
    {
        private BddContext _bddContext;
        public DalPersonManagement()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        //-------------------------------------------------------------------------------------------------

        #region CRUD Person

        public List<Person> GetAllPeople()
        {
            return _bddContext.People.ToList();
        }

        //Create Person
        public int CreatePerson(string name, string lastName)
        {
            Person person = new Person() { Name = name, LastName = lastName, ProfilePicturePath= "/ProfilePictures/ProfileGif1.gif" };
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
        public void UpdatePerson(int id, string name, string lastName, string profilePicturePath)
        {
            Person person = _bddContext.People.Find(id);

            if (person != null)
            {
                person.Id = id;
                person.Name = name;
                person.LastName = lastName;
                person.ProfilePicturePath = profilePicturePath;
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
        #endregion

        //-------------------------------------------------------------------------------------------------

        # region CRUD User
        public List<User> GetAllUsers()
        {
            List<User> listUsers = _bddContext.Users.Include(e => e.BankDetails).Include(e => e.BillingAddress).Include(e => e.Person).Include(e => e.Account).Include(e => e.Vehicule).Include(e => e.EcoWallet).Include(e => e.ShoppingCart).ToList();
            return listUsers;
        }

        public User GetUser(int id)
        {
            return _bddContext.Users.Include(e => e.BankDetails).Include(e => e.BillingAddress).Include(e => e.Person).Include(e => e.Account).Include(e => e.Vehicule).Include(e => e.EcoWallet).Include(e => e.ShoppingCart).FirstOrDefault(e => e.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            User user = _bddContext.Users.Include(e => e.BankDetails).Include(e => e.BillingAddress).Include(e => e.Person).Include(e => e.Account).Include(e => e.Vehicule).Include(e => e.EcoWallet).Include(e => e.ShoppingCart).FirstOrDefault(e => e.Email == email);
            return user;
        }

        //Create User
        public int CreateUser(
            string email,
            DateTime birthDate,
            int phoneNumber,
            int identityCardNumber,
            int drivingPermitNumber,
            double userRating,
            EcoStatusType selectEcoStatusType,
            int bankDetailsId,
            int billingAddressId,
            int personId,
            int? vehiculeId,
            int? ecoWalletId,
            int? shoppingCartId,
            int? accountId
            )
        {
            User user = new User()
            {
                Email = email,
                BirthDate = birthDate,
                PhoneNumber = phoneNumber,
                IdentityCardNumber = identityCardNumber,
                DrivingPermitNumber = drivingPermitNumber,
                UserRating = userRating,
                SelectEcoStatusType = 0,
                BankDetails = _bddContext.BankingDetails.First(b => b.Id == bankDetailsId),
                BillingAddress = _bddContext.BillingAddresses.First(b => b.Id == billingAddressId),
                Person = _bddContext.People.First(b => b.Id == personId),
                Vehicule = _bddContext.Vehicules.FirstOrDefault(b => b.Id == vehiculeId),
                EcoWallet = _bddContext.EcoWallets.FirstOrDefault(b => b.Id == ecoWalletId),
                ShoppingCart = _bddContext.ShoppingCarts.FirstOrDefault(b => b.Id == shoppingCartId),
                Account = _bddContext.Accounts.FirstOrDefault(b => b.Id == accountId)
            };
            _bddContext.Users.Add(user);
            _bddContext.SaveChanges();
            return user.Id;
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
            int personId,
            int? vehiculeId,
            int? ecoWalletId,
            int? shoppingcartId,
            int? accountId

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
                userToUpdate.DrivingPermitNumber = drivingPermitNumber;
                userToUpdate.BankDetails = _bddContext.BankingDetails.FirstOrDefault(b => b.Id == bankDetailsId);
                userToUpdate.BillingAddress = _bddContext.BillingAddresses.FirstOrDefault(b => b.Id == billingAddressId);
                userToUpdate.Person = _bddContext.People.FirstOrDefault(b => b.Id == personId);
                userToUpdate.Vehicule = _bddContext.Vehicules.FirstOrDefault(b => b.Id == vehiculeId);
                userToUpdate.EcoWallet = _bddContext.EcoWallets.FirstOrDefault(b => b.Id == ecoWalletId);
                userToUpdate.ShoppingCart = _bddContext.ShoppingCarts.FirstOrDefault(b => b.Id == shoppingcartId);
                userToUpdate.Account = _bddContext.Accounts.FirstOrDefault(b => b.Id == accountId);
                _bddContext.SaveChanges();
            }
        }
        public void UpdateUser(User user)
        {
            _bddContext.Users.Update(user);
            _bddContext.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            User user = _bddContext.Users.Find(id);

            if (user != null)
            {
                _bddContext.Users.Remove(user);
                _bddContext.SaveChanges();
            }
        }
        #endregion

        //-------------------------------------------------------------------------------------------------

        # region CRUD Administrator
        public List<Administrator> GetAllAdministrators()
        {
            return _bddContext.Administrators.ToList();
        }

        public Administrator GetAdministrator(int id)
        {
            return _bddContext.Administrators.FirstOrDefault(e => e.Id == id);
        }

        public Administrator GetAdministrator(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAdministrator(id);
            }
            return null;
        }

        public Administrator AuthentifyAdministrator(string username, string passwordClear)
        {
            string password = EncodeMD5(passwordClear);
            Administrator administrator = this._bddContext.Administrators.FirstOrDefault(a => a.Username == username && a.Password == password);
            return administrator;
        }

        //Create Administrator
        public int CreateAdministrator(string username, string passwordClear, string emailPro, int phoneNumberPro, string employeeCode)
        {
            string password = EncodeMD5(passwordClear);
            Administrator administrator = new Administrator()
            {
                Username = username,
                Password = passwordClear,
                EmailPro = emailPro,
                PhoneNumberPro = phoneNumberPro,
                EmployeeCode = employeeCode
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
        public void UpdateAdministrator(int id, string username, string password, string emailPro, int phoneNumberPro, string employeeCode)
        {
            Administrator administrator = _bddContext.Administrators.Find(id);

            if (administrator != null)
            {
                administrator.Id = id;
                administrator.Username = username;
                administrator.Password = password;
                administrator.EmailPro = emailPro;
                administrator.PhoneNumberPro = phoneNumberPro;
                administrator.EmployeeCode = employeeCode;
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
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Account
        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = _bddContext.Accounts.ToList();
            return accounts;
        }

        public Account GetUserAccount(int id)
        {

            User user = _bddContext.Users.Find(id);
            Account account = _bddContext.Accounts.FirstOrDefault(e => e.Id == user.AccountId);
            return account;
        }

        public Account GetAccount(int id)
        {


            return _bddContext.Accounts.FirstOrDefault(e => e.Id == id);

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
        public int CreateAccount(string username, string passwordClear, bool isActive, DateTime creationDate, DateTime lastLoginDate)
        {
            string password = EncodeMD5(passwordClear);
            Account account = new Account()
            {
                Username = username,
                Password = password,
                IsActive = true,
                CreationDate = creationDate,
                LastLoginDate = lastLoginDate,
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
        public void UpdateAccount(int id, string username, string password, bool isActive, DateTime lastLoginDate)
        {
            string passwordEncrypted = EncodeMD5(password);
            Account account = _bddContext.Accounts.Find(id);

            if (account != null)
            {
                account.Id = id;
                account.Username = username;
                account.Password = passwordEncrypted;
                account.IsActive = isActive;
                account.LastLoginDate = lastLoginDate;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAccountWithoutEncodingPassword(int id, string username, string password, bool isActive, DateTime lastLoginDate)
        {
            Account account = _bddContext.Accounts.Find(id);

            if (account != null)
            {
                account.Id = id;
                account.Username = username;
                account.Password = password;
                account.IsActive = isActive;
                account.LastLoginDate = lastLoginDate;
                _bddContext.SaveChanges();
            }
        }
        public void UpdateAccountPassword(int id, string password)
        {
            string passwordEncrypted = EncodeMD5(password);
            Account account = _bddContext.Accounts.Find(id);
            if (account != null)
            {
                account.Id = id;
                account.Password = passwordEncrypted;
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
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Vehicule
        public List<Vehicule> GetAllVehicules()
        {
            return _bddContext.Vehicules.Include(e => e.Insurance).ToList();
        }

        public Vehicule GetUserVehicule(int userId)
        {
            User user = _bddContext.Users.Find(userId);
            Vehicule vehicule = _bddContext.Vehicules.Include(e => e.Insurance).FirstOrDefault(e => e.Id == user.VehiculeId);
            return vehicule;
        }

        //Create Vehicule
        public int CreateVehicule(string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId)
        {
            Vehicule vehicule = new Vehicule()
            {
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
            return vehicule.Id;
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
        #endregion

        //-------------------------------------------------------------------------------------------------

        #region CRUD Insurance
        public List<Insurance> GetAllInsurances()
        {
            return _bddContext.Insurances.ToList();
        }

        public Insurance GetInsurance(int insuranceId)
        {
            return _bddContext.Insurances.FirstOrDefault(a => a.Id == insuranceId);
        }
        public Insurance GetUserInsurance(int userId)
        {
            User user = _bddContext.Users.FirstOrDefault(x => x.Id == userId);
            Vehicule vehicule = _bddContext.Vehicules.Find(user.VehiculeId);
            Insurance insurance = _bddContext.Insurances.Find(vehicule.InsuranceId);
            return insurance;
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
        #endregion

        //-------------------------------------------------------------------------------------------------

    }
}