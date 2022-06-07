using EcoCar.Models.PersonManagement;
using System;
using System.Collections.Generic;

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
        void UpdateUser(int id, string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber);
        void DeleteUser(int id);

        //-------------------------------------------------------------------------------------------------

        //Administrator
        List<Administrator> GetAllAdministrators();
        int CreateAdministrator(string emailPro, int phoneNumberPro);
        void UpdateAdministrator(int id, string emailPro, int phoneNumberPro);
        void DeleteAdministrator(int id);

        //-------------------------------------------------------------------------------------------------

        //Account
        List<Account> GetAllAccounts();
        int CreateAccount(string username, string password, bool isActive, int personId);
        void UpdateAccount(int id, string username, string password, bool isActive);
        void DeleteAccount(int id);

        //-------------------------------------------------------------------------------------------------

        //AccountUser
        List<AccountUser> GetAllAccountUsers();
        int CreateAccountUser(double userRating, int EcoStatusId);
        void UpdateAccountUser(int id, double userRating, int EcoStatusIds);
        void DeleteAccountUser(int id);

        Account Authentify(string username, string passwordClear);
        Account GetAccount(int id);
        Account GetAccount(string idStr);

        //-------------------------------------------------------------------------------------------------

        //AccountAdministrator
        List<AccountAdministrator> GetAllAccountAdministrators();
        int CreateAccountAdministrator(string employeeCode);
        void UpdateAccountAdministrator(int id, string employeeCode);
        void DeleteAccountAdministrator(int id);

        //-------------------------------------------------------------------------------------------------

        //Vehicule
        List<Vehicule> GetAllVehicules();
        int CreateVehicule(string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats);
        void UpdateVehicule(int id, string brand, int registrationNumber, string model, bool hybrid, bool electric, DateTime technicalTestExpiration, int availableSeats);
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
