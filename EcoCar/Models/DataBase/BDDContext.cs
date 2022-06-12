﻿using EcoCar.Models.FinancialManagement;
using EcoCar.Models.ServiceManagement;
using EcoCar.Models.MessagingManagement;
using Microsoft.EntityFrameworkCore;
using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.DataBase
{
    public class BddContext : DbContext
    {
        //Person Management
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUser> AccountUsers { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Insurance> Insurances { get; set; }

        //Financial Management
        public DbSet<EcoWallet> EcoWallets { get; set; }
        public DbSet<BankDetails> BankingDetails { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<EcoStore> EcoStores { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ServiceInvoice> ServiceInvoices { get; set; }
        public DbSet<EcoStoreInvoice> EcoStoreInvoices { get; set; }

        //Service Management
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceRequestFinal> ServiceRequestsFinal { get; set; }
        public DbSet<CarPoolingService> CarPoolingServices { get; set; }
        public DbSet<ParcelService> ParcelServices { get; set; }
        public DbSet<CarRentalService> CarRentalServices { get; set; }
        public DbSet<Trajectory> Trajectories { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        //Messaging Management
        public DbSet<Message> Messages { get; set; }
        public DbSet<Reporting> Reportings { get; set; }
        public DbSet<HelpReporting> HelpReportings { get; set; }
        public DbSet<UserReporting> UserReportings { get; set; }
        public DbSet<AdministratorResponse> AdministratorResponses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=EcoCar");
        }



        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            //Admin
            this.Administrators.Add(
                new Administrator
                {
                    Id = 1,
                    Username = "Admin",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random,
                    EmailPro = "Admin@EcoCar.com",
                    PhoneNumberPro = 0764958674,
                    EmployeeCode = "AD123"
                }
                );

            //User1
            this.People.Add(
                new Person
                {
                    Id = 1,
                    Name = "RandomName",
                    LastName = "RandomLastName",
                    ProfilePictureURL = "wololo.jpg"
                }
                );
            this.Users.Add(
                new User
                {
                    Id = 1,
                    Email = "random@ecocar.com",
                    PhoneNumber = 1234457891,
                    IdentityCardNumber = 1234535315,
                    DrivingPermitNumber = 1230432153,
                    BankDetailsId = 1,
                    BillingAddressId = 1,
                    PersonId = 1,
                    VehiculeId = 1,
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 1,
                    Username = "random",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    IsActive = true,
                    PersonId = 1
                });
            this.AccountUsers.Add(
                new AccountUser
                {
                    Id = 1,
                    UserRating = 4,
                    AccountId = 1,
                    EcoWalletId = 1,
                    VehiculeId = 1
                });
            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 1,
                    BankName = "SwissBank",
                    Swift = "OABGAOG",
                    Iban = "FIOAFNEOAIFNAFKNAEFOJKN°31234"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 1,
                    AddressLine = "403, Salty Road",
                    City = "Brest",
                    Region = "SaltyAlpes",
                    Country = "France",
                    PostalCode = 40404
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 1,
                    EcoCoinsAmount = 101,
                    Subscription = false,
                    EcoCoinsValueEuros = 41
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 1,
                    Brand = "AnAmazingBrand",
                    RegistrationNumber = 001,
                    Model = "Teslite",
                    Hybrid = true,
                    Electric = false,
                    AvailableSeats = 3,
                    InsuranceId = 1,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 1,
                    InsuranceAgency = "OFIBNA",
                    ContractNumber = "R124124124TRAT"
                });

            //User2
            this.People.Add(
            new Person
            {
                Id = 2,
                Name = "Farah",
                LastName = "Farah",
                ProfilePictureURL = "Farah.jpg"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 2,
                    Email = "Farah@ecocar.com",
                    PhoneNumber = 1234457891,
                    IdentityCardNumber = 1234535315,
                    DrivingPermitNumber = 1230432153,
                    BankDetailsId = 1,
                    BillingAddressId = 1,
                    PersonId = 1,
                    VehiculeId = 1,
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 2,
                    Username = "Farah",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    IsActive = true,
                    PersonId = 1
                });
            this.AccountUsers.Add(
                new AccountUser
                {
                    Id = 2,
                    UserRating = 4,
                    AccountId = 1,
                    EcoWalletId = 1,
                    VehiculeId = 1
                });
            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 2,
                    BankName = "SwissBank",
                    Swift = "OABGAOG",
                    Iban = "FIOAFNEOAIFNAFKNAEFOJKN°31234"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 2,
                    AddressLine = "403, Salty Road",
                    City = "Brest",
                    Region = "SaltyAlpes",
                    Country = "France",
                    PostalCode = 40404
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 2,
                    EcoCoinsAmount = 101,
                    Subscription = false,
                    EcoCoinsValueEuros = 41
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 2,
                    Brand = "AnAmazingBrand",
                    RegistrationNumber = 001,
                    Model = "Teslite",
                    Hybrid = true,
                    Electric = false,
                    AvailableSeats = 3,
                    InsuranceId = 1,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 2,
                    InsuranceAgency = "OFIBNA",
                    ContractNumber = "R124124124TRAT"
                });
            this.SaveChanges();
        }




        //Defining character length properties of each table

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Person>(entity =>
        //    {
        //        entity.Property(p => p.Name)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(p => p.LastName)
        //            .HasColumnType("varchar(20)");
        //    });

        //    modelBuilder.Entity<User>(entity =>
        //    {
        //        entity.Property(u => u.Email)
        //            .HasColumnType("varchar(40)");
        //        entity.Property(u => u.PhoneNumber)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.IdentityCardNumber)
        //           .HasColumnType("varchar(14)");
        //        entity.Property(u => u.DrivingPermitNumber)
        //           .HasColumnType("varchar(9)");
        //    });

        //    modelBuilder.Entity<Administrator>(entity =>
        //    {
        //        entity.Property(u => u.Username)
        //            .HasColumnType("varchar(10)");
        //        entity.Property(u => u.Password)
        //           .HasColumnType("varchar(20)");
        //        entity.Property(u => u.EmailPro)
        //           .HasColumnType("varchar(40)");
        //        entity.Property(u => u.PhoneNumberPro)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.EmployeeCode)
        //           .HasColumnType("varchar(5)");
        //    });

        //    modelBuilder.Entity<Account>(entity =>
        //    {
        //        entity.Property(u => u.Username)
        //            .HasColumnType("varchar(10)");
        //        entity.Property(u => u.Password)
        //           .HasColumnType("varchar(20)");
        //    });

        //    modelBuilder.Entity<AccountUser>(entity =>
        //    {
        //        entity.Property(u => u.UserRating)
        //            .HasColumnType("varchar(5)");
        //        entity.Property(u => u.SelectEcoStatusType)
        //           .HasColumnType("varchar(15)");
        //    });

        //    modelBuilder.Entity<Insurance>(entity =>
        //    {
        //        entity.Property(u => u.InsuranceAgency)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.ContractNumber)
        //           .HasColumnType("varchar(10)");
        //    });

        //    modelBuilder.Entity<Vehicule>(entity =>
        //    {
        //        entity.Property(u => u.Brand)
        //            .HasColumnType("varchar(10)");
        //        entity.Property(u => u.RegistrationNumber)
        //           .HasColumnType("varchar(15)");
        //        entity.Property(u => u.Model)
        //            .HasColumnType("varchar(10)");
        //        entity.Property(u => u.AvailableSeats)
        //           .HasColumnType("varchar(5)");
        //    });

        //    modelBuilder.Entity<CarPoolingService>(entity =>
        //    {
        //        entity.Property(u => u.SelectCarPoolingType)
        //            .HasColumnType("varchar(15)");
        //        entity.Property(u => u.AvailableSeats)
        //           .HasColumnType("varchar(5)");
        //    });

        //    modelBuilder.Entity<CarRentalService>(entity =>
        //    {
        //        entity.Property(u => u.KeyPickUpAddress)
        //            .HasColumnType("varchar(30)");
        //        entity.Property(u => u.KeyDropOffAddress)
        //           .HasColumnType("varchar(30)");
        //    });

        //    modelBuilder.Entity<Itinerary>(entity =>
        //    {
        //        entity.Property(u => u.FirtsStopAddress)
        //            .HasColumnType("varchar(30)");
        //        entity.Property(u => u.SecondStopAddress)
        //           .HasColumnType("varchar(30)");
        //        entity.Property(u => u.ThirdStopAddress)
        //           .HasColumnType("varchar(30)");
        //    });

        //    modelBuilder.Entity<ParcelService>(entity =>
        //    {
        //        entity.Property(u => u.BarCode)
        //            .HasColumnType("varchar(15)");
        //        entity.Property(u => u.WeightKilogrammes)
        //           .HasColumnType("varchar(15)");
        //    });

        //    modelBuilder.Entity<Service>(entity =>
        //    {
        //        entity.Property(u => u.ReferenceNumber)
        //            .HasColumnType("varchar(15)");
        //        entity.Property(u => u.SelectServiceType)
        //           .HasColumnType("varchar(20)");
        //    });

        //    modelBuilder.Entity<Trajectory>(entity =>
        //    {
        //        entity.Property(u => u.DurationHours)
        //            .HasColumnType("varchar(5)");
        //        entity.Property(u => u.StopNumber)
        //           .HasColumnType("varchar(5)");
        //        entity.Property(u => u.StopsDurationMinutes)
        //           .HasColumnType("varchar(5)");
        //        entity.Property(u => u.PickUpAddress)
        //           .HasColumnType("varchar(30)");
        //        entity.Property(u => u.DeliveryAddress)
        //           .HasColumnType("varchar(30)");
        //        entity.Property(u => u.SelectTrajectoryType)
        //           .HasColumnType("varchar(15)");
        //    });

        //    modelBuilder.Entity<BankDetails>(entity =>
        //    {
        //        entity.Property(u => u.BankName)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.Swift)
        //           .HasColumnType("varchar(15)");
        //        entity.Property(u => u.Iban)
        //           .HasColumnType("varchar(15)");
        //    });

        //    modelBuilder.Entity<BillingAddress>(entity =>
        //    {
        //        entity.Property(u => u.AddressLine)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.City)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.Region)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.Country)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.PostalCode)
        //           .HasColumnType("varchar(5)");
        //    });

        //    modelBuilder.Entity<EcoStore>(entity =>
        //    {
        //        entity.Property(u => u.EcoCoinsBatchOnePrice)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.EcoCoinsBatchTwoPrice)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.EcoCoinsBatchThreePrice)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.MonthlySubscriptionPrice)
        //           .HasColumnType("varchar(10)");
        //        entity.Property(u => u.TrimestrialSubscriptionPrice)
        //           .HasColumnType("varchar(5)");
        //        entity.Property(u => u.SemestrialSubscriptionPrice)
        //           .HasColumnType("varchar(5)");
        //    });


        //    modelBuilder.Entity<EcoWallet>(entity =>
        //    {
        //        entity.Property(u => u.EcoCoinsAmount)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.EcoCoinsValueEuros)
        //           .HasColumnType("varchar(10)");
        //    });

        //    modelBuilder.Entity<Invoice>(entity =>
        //    {
        //        entity.Property(u => u.InvoiceNumber)
        //            .HasColumnType("varchar(20)");
        //        entity.Property(u => u.InvoiceDescription)
        //           .HasColumnType("varchar(50)");
        //        entity.Property(u => u.SelectInvoiceType)
        //           .HasColumnType("varchar(20)");
        //    });
        //}
    }
}
