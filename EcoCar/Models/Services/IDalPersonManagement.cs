using EcoCar.Models.PersonManagement;
using System;
using System.Collections.Generic;
using static EcoCar.Models.PersonManagement.AccountUser;

namespace EcoCar.Models.Services
{
    public interface IDalPersonManagement : IDisposable
    {
        //Person
        List<Person> GetAllPeople();
        int CreatePerson(string name, string lastName, string profilePictureURL);
        void UpdatePerson(int id, string name, string lastName, string profilePictureURL);
        void DeletePerson (int id);

        //-------------------------------------------------------------------------------------------------

        //User
        List<User> GetAllUsers();
        User CreateUser(string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, int bankDetailsId, int billingAddressId, int personId);
        void UpdateUser(int id, string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, int bankDetailsId, int billingAddressId, int personId);
        void DeleteUser(int id);

        //-------------------------------------------------------------------------------------------------

        //Administrator
        List<Administrator> GetAllAdministrators();
        int CreateAdministrator(string emailPro, int phoneNumberPro, int personId);
        void UpdateAdministrator(int id, string emailPro, int phoneNumberPro, int personId);
        void DeleteAdministrator(int id);

        //-------------------------------------------------------------------------------------------------

        //Account
        List<Account> GetAllAccounts();
        int CreateAccount(string username, string password, bool isActive, int personId);
        void UpdateAccount(int id, string username, string password, bool isActive, int personId);
        void DeleteAccount(int id);
        Account Authentify(string username, string passwordClear);
        Account GetAccount(int id);
        Account GetAccount(string idStr);

        //-------------------------------------------------------------------------------------------------

        //AccountUser
        List<AccountUser> GetAllAccountUsers();
        int CreateAccountUser(double userRating, EcoStatusType selectEcoStatusType, int ecoWalletId, int vehiculeId, int accountId);
        void UpdateAccountUser(int id, double userRating, EcoStatusType selectEcoStatusType, int ecoWalletId, int vehiculeId, int accountId);
        void DeleteAccountUser(int id);

        //-------------------------------------------------------------------------------------------------

        //AccountAdministrator
        List<AccountAdministrator> GetAllAccountAdministrators();
        int CreateAccountAdministrator(string employeeCode, int accountId);
        void UpdateAccountAdministrator(int id, string employeeCode, int accountId);
        void DeleteAccountAdministrator(int id);

        //-------------------------------------------------------------------------------------------------

        //Vehicule
        List<Vehicule> GetAllVehicules();
        Vehicule CreateVehicule(string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats, int insuranceId);
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
