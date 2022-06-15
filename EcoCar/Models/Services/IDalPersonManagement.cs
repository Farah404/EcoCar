using EcoCar.Models.PersonManagement;
using System;
using System.Collections.Generic;
using static EcoCar.Models.PersonManagement.User;

namespace EcoCar.Models.Services
{
    public interface IDalPersonManagement : IDisposable
    {
        //Person
        List<Person> GetAllPeople();
        int CreatePerson(string name, string lastName);
        void UpdatePerson(int id, string name, string lastName, string ProfilePicturePath);
        void DeletePerson (int id);

        //-------------------------------------------------------------------------------------------------

        //User
        List<User> GetAllUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        int CreateUser(string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, double userRating, EcoStatusType selectEcoStatusType, int bankDetailsId, int billingAddressId, int personId, int? vehiculeId, int? ecoWalletId, int? shoppingCartId, int? accountId);
        void UpdateUser(int id, string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, int bankDetailsId, int billingAddressId, int personId, int? vehiculeId, int? ecoWalletId, int? shoppingCartId, int? accountId);
        void DeleteUser(int id);
        void UpdateUserVehicule(int userId, int vehiculeId);

        //-------------------------------------------------------------------------------------------------

        //Administrator
        List<Administrator> GetAllAdministrators();
        int CreateAdministrator(string username, string password, string emailPro, int phoneNumberPro, string employeeCode);
        void UpdateAdministrator(int id, string username, string password, string emailPro, int phoneNumberPro, string employeeCode);
        void DeleteAdministrator(int id);
        Administrator AuthentifyAdministrator(string username, string passwordClear);
        Administrator GetAdministrator(int id);
        Administrator GetAdministrator(string idStr);

        //-------------------------------------------------------------------------------------------------

        //Account
        List<Account> GetAllAccounts();
        int CreateAccount(string username, string password, bool isActive, DateTime creationDate, int personId);
        void UpdateAccount(int id, string username, string password, bool isActive, int personId);
        void UpdateAccountPassword(int id, string password);
        void DeleteAccount(int id);
        Account Authentify(string username, string passwordClear);
        Account GetAccount(int id);
        Account GetAccount(string idStr);
        Account GetUserAccount(int id);

        //-------------------------------------------------------------------------------------------------


        //Vehicule
        List<Vehicule> GetAllVehicules();
        Vehicule GetUserVehicule(int id);
        int CreateVehicule(string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId);
        void UpdateVehicule(int id, string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId);
        void DeleteVehicule(int id);

        //-------------------------------------------------------------------------------------------------

        //Insurance
        List<Insurance> GetAllInsurances();
        int CreateInsurance(string insuranceAgency, DateTime insuranceExpiration, string contractNumber);
        void UpdateInsurance(int id, string insuranceAgency, DateTime insuranceExpiration, string contractNumber);
        void DeleteInsurance(int id);

        //-------------------------------------------------------------------------------------------------


    }
}
