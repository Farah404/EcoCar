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
        int CreatePerson(string name, string lastName, string profilePictureURL);
        void UpdatePerson(int id, string name, string lastName, string profilePictureURL);
        void DeletePerson (int id);

        //-------------------------------------------------------------------------------------------------

        //User
        List<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, double userRating, EcoStatusType selectEcoStatusType, int bankDetailsId, int billingAddressId, int personId, int? vehiculeId, int? ecoWalletId, int? accountId);
        void UpdateUser(int id, string email, DateTime birthDate, int phoneNumber, int identityCardNumber, int drivingPermitNumber, int bankDetailsId, int billingAddressId, int personId, int? vehiculeId, int? ecoWalletId, int? accountId);
        void DeleteUser(int id);

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
        int CreateAccount(string username, string password, bool isActive, int personId);
        void UpdateAccount(int id, string username, string password, bool isActive, int personId);
        void DeleteAccount(int id);
        Account Authentify(string username, string passwordClear);
        Account GetAccount(int id);
        Account GetAccount(string idStr);

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
