using EcoCar.Models.FinancialManagement;
using EcoCar.Models.ServiceManagement;
using EcoCar.Models.MessagingManagement;
using Microsoft.EntityFrameworkCore;
using EcoCar.Models.PersonManagement;
using System;

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
        public DbSet<AccountAdministrator> AccountAdministrators { get; set; }
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
        public DbSet<CarPoolingService> CarPoolingServices { get; set; }
        public DbSet<ParcelService> ParcelServices { get; set; }
        public DbSet<CarRentalService> CarRentalServices { get; set; }
        public DbSet<Trajectory> Trajectories { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }

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
                    PersonId = 1
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
                    InsuranceID = 1
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 1,
                    InsuranceAgency = "OFIBNA",
                    ContractNumber = "R124124124TRAT"
                });
            this.SaveChanges();

            this.Itineraries.AddRange(
                new Itinerary
                {
                    Id = 100,
                    FirtsStopAddress = "1, Rue B, 13000 Marseille",
                    //SecondStopAddress = "1, Rue C, 13000 Marseille",
                    //ThirdStopAddress = "1, Rue D, 13000 Marseille"
                },

                new Itinerary
                {
                    Id = 300,
                    FirtsStopAddress = "2, Rue B, 13100 Aix en Provence",
                    //SecondStopAddress = "1, Rue C, 13000 Marseille",
                    //ThirdStopAddress = "1, Rue D, 13000 Marseille"
                });
            this.SaveChanges();


            this.Trajectories.AddRange(

                new Trajectory
                {
                    Id = 100,
                    DurationHours = (int)1.5,
                    StopNumber = 1,
                    StopsDurationMinutes = 30,
                    PickUpAddress = "1, Rue A, 13000 Marseille",
                    DeliveryAddress = "2, Rue A, 13100 Aix en Provence",
                    SelectTrajectoryType = Trajectory.TrajectoryType.Regular,
                    ItineraryId = 100

                },

                new Trajectory
                {
                    Id = 300,
                    DurationHours = (int)1.5,
                    StopNumber = 1,
                    StopsDurationMinutes = 5,
                    PickUpAddress = "1, Rue A, 13000 Marseille",
                    DeliveryAddress = "2, Rue A, 13100 Aix en Provence",
                    SelectTrajectoryType = Trajectory.TrajectoryType.Punctual,
                    ItineraryId = 300

                });
            this.SaveChanges();

            


            this.Services.AddRange(

                new Service
                {
                    Id=100,
                    IdServiceProvider = 100,
                    PublicationDate= new DateTime(2022, 06, 09),
                    ExpirationDate = new DateTime(2022, 06, 18),
                    ReferenceNumber = 100,
                   // Isexpired=false,
                   Start = new DateTime(2022, 06, 20),
                   End = new DateTime(2022,06,20),

                   SelectServiceType = Service.ServiceType.CarPoolingService
                },


                new Service
                {
                    Id = 200,
                    IdServiceProvider = 200,
                    PublicationDate = new DateTime(2022, 06, 11),
                    ExpirationDate = new DateTime(2022, 06, 20),
                    ReferenceNumber = 100,
                    // Isexpired=false,
                    Start = new DateTime(2022, 06, 22),
                    End = new DateTime(2022, 06, 22),

                    SelectServiceType = Service.ServiceType.CarRentalService
                },


                new Service
                {
                    Id = 300,
                    IdServiceProvider = 300,
                    PublicationDate = new DateTime(2022, 06, 10),
                    ExpirationDate = new DateTime(2022, 06, 19),
                    ReferenceNumber = 300,
                    // Isexpired=false,
                    Start = new DateTime(2022, 06, 21),
                    End = new DateTime(2022, 06, 21),

                    SelectServiceType = Service.ServiceType.ParcelService
                });
            this.SaveChanges();
            
            
            this.CarPoolingServices.Add(

                new CarPoolingService
                {
                    Id=100,
                    ServiceId=100,
                    SelectCarPoolingType = CarPoolingService.CarPoolingType.HomeToWork,
                    AvailableSeats=3,
                    PetsAllowed = false,
                    SmokingAllowed = false,
                    MusicAllowed = true,
                    ChattingAllowed=false,
                    TrajectoryId=100

                });
            this.SaveChanges();


            this.CarRentalServices.Add(

                new CarRentalService
                {
                    Id = 200,
                    ServiceId = 200,
                    KeyPickUpAddress = "1, Rue A, 13000 Marseille",
                    KeyDropOffAddress = "1, Rue A, 13000 Marseille",
                    VehiculeId = 1,


                });

            this.ParcelServices.Add(

                new ParcelService
                {
                    Id = 300,
                    ServiceId = 300,
                    BarCode = 300000,
                    WeightKilogrammes = 5,
                    AtypicalVolume = true,
                    Fragile = false,
                    TrajectoryId = 300,

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
        //            .HasColumnType("varchar(100)");
        //    });
        //}
    }
}
