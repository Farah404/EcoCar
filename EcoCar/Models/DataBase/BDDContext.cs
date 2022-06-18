using EcoCar.Models.FinancialManagement;
using EcoCar.Models.ServiceManagement;
using EcoCar.Models.MessagingManagement;
using Microsoft.EntityFrameworkCore;
using EcoCar.Models.PersonManagement;
using Microsoft.Extensions.Configuration;
using System;

namespace EcoCar.Models.DataBase
{
    public class BddContext : DbContext
    {
        #region Person Management tables
        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Vehicule> Vehicules { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        #endregion
        #region Financial Management tables
        public DbSet<EcoWallet> EcoWallets { get; set; }
        public DbSet<BankDetails> BankingDetails { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
        public DbSet<EcoStore> EcoStores { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ServiceInvoice> ServiceInvoices { get; set; }
        public DbSet<EcoStoreInvoice> EcoStoreInvoices { get; set; }
        #endregion
        #region Service Management tables
        public DbSet<Service> Services { get; set; }
        public DbSet<CarPoolingService> CarPoolingServices { get; set; }
        public DbSet<ParcelService> ParcelServices { get; set; }
        public DbSet<CarRentalService> CarRentalServices { get; set; }
        public DbSet<Trajectory> Trajectories { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        #endregion
        #region Messagins Management tables
        public DbSet<Message> Messages { get; set; }
        //public DbSet<Reporting> Reportings { get; set; }
        //public DbSet<HelpReporting> HelpReportings { get; set; }
        //public DbSet<UserReporting> UserReportings { get; set; }
        //public DbSet<AdministratorResponse> AdministratorResponses { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;password=rootine;database=EcoCar");
            }
            else
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }
        }



        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            #region Initializing the EcoStore
            this.EcoStores.Add(
                new EcoStore
                {
                    Id = 1,

                    EcoCoinsBatchOnePrice = 10.00,
                    EcoCoinsBatchOne = 20,
                    NameOne = "EcoCoinsBatchOne",

                    EcoCoinsBatchTwoPrice = 18.00,
                    EcoCoinsBatchTwo = 40,
                    NameTwo = "EcoCoinsBatchTwo",

                    EcoCoinsBatchThreePrice = 35.00,
                    EcoCoinsBatchThree = 60,
                    NameThree = "EcoCoinsBatchThree",


                    MonthlySubscriptionPrice = 10.00,
                    MonthlySubscription = 30,
                    NameMonth = "MonthlySubscription",

                    TrimestrialSubscriptionPrice = 50.00,
                    TrimestrialSubscription = 30,
                    NameTrimester = "TrimestrialSubscription",

                    SemestrialSubscriptionPrice = 70.00,
                    SemestrialSubscription = 30,
                    NameSemester = "SemestrialSubscription",
                });


            #endregion

            #region Initializing the admins
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
            #endregion

            #region  Initializing User1: Hermann/random
            this.People.Add(
                new Person
                {
                    Id = 1,
                    Name = "Hermann",
                    LastName = "Gauduin",
                    ProfilePicturePath = "/ProfilePictures/ProfileGif10.gif"
                }
                );
            this.Users.Add(
                new User
                {
                    Id = 1,
                    Email = "H.gauduin@ecocar.com",
                    BirthDate = new DateTime(1993, 04, 21),
                    PhoneNumber = 0764241035,
                    IdentityCardNumber = 85649325,
                    DrivingPermitNumber = 5239458,
                    UserRating = 4.8,
                    SelectEcoStatusType = User.EcoStatusType.EcoForest,
                    BankDetailsId = 1,
                    BillingAddressId = 1,
                    PersonId = 1,
                    VehiculeId = 1,
                    AccountId = 1,
                    EcoWalletId = 1,
                    ShoppingCartId = 1,
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 1,
                    Username = "Hermann",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2021, 02, 18),
                    LastLoginDate = new DateTime(2022, 06, 16),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 1,
                    BankName = "Banque Postale",
                    Swift = "AE298",
                    Iban = "FR7612548029989876543210917"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 1,
                    AddressLine = "3, rue Amiral Labrousse",
                    City = "Brest",
                    Region = "Bretagne",
                    Country = "France",
                    PostalCode = 29200
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 1,
                    EcoCoinsAmount = 54,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 08, 18),
                    SubscriptionStart = new DateTime(2022, 06, 18),

                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 1,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 1,
                    Brand = "Renault",
                    RegistrationNumber = 001004005,
                    Model = "Laguna",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 1,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 1,
                    InsuranceAgency = "MAIF",
                    ContractNumber = "R124124124TRAT"
                });
            #endregion

            #region Initializing User2: Farah/random
            this.People.Add(
            new Person
            {
                Id = 2,
                Name = "Farah",
                LastName = "Gauduin",
                ProfilePicturePath = "/ProfilePictures/ProfileGif17.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 2,
                    Email = "Farah@ecocar.com",
                    PhoneNumber = 0764351024,
                    IdentityCardNumber = 123456987,
                    DrivingPermitNumber = 951753456,
                    BirthDate = new DateTime(1992, 09, 05),
                    UserRating = 4.6,
                    SelectEcoStatusType = User.EcoStatusType.EcoForest,
                    BankDetailsId = 2,
                    BillingAddressId = 2,
                    PersonId = 2,
                    VehiculeId = 2,
                    AccountId = 2,
                    EcoWalletId = 2,
                    ShoppingCartId = 2
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 2,
                    Username = "Farah",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2021, 05, 06),
                    LastLoginDate = new DateTime(2022, 06, 05),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 2,
                    BankName = "Banque Postale",
                    Swift = "AE298",
                    Iban = "FR7612548029989876543210917"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 2,
                    AddressLine = "3, rue Amiral Labrousse",
                    City = "Brest",
                    Region = "Bretagne",
                    Country = "France",
                    PostalCode = 29200
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 2,
                    EcoCoinsAmount = 150,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 12, 15),
                    SubscriptionStart = new DateTime(2022, 01, 15),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 2,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 2,
                    Brand = "Tesla",
                    RegistrationNumber = 439872654,
                    Model = "Model Y",
                    Hybrid = false,
                    Electric = true,
                    AvailableSeats = 4,
                    InsuranceId = 2,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 2,
                    InsuranceAgency = "MAIF",
                    ContractNumber = "CO1569TR954"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User3: Francois/random
            this.People.Add(
            new Person
            {
                Id = 3,
                Name = "François-Noël",
                LastName = "Bernal",
                ProfilePicturePath = "/ProfilePictures/ProfileGif5.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 3,
                    Email = "François@ecocar.com",
                    PhoneNumber = 0695843625,
                    IdentityCardNumber = 2939099,
                    DrivingPermitNumber = 8619077,
                    BirthDate = new DateTime(1991, 08, 15),
                    UserRating = 4.5,
                    SelectEcoStatusType = User.EcoStatusType.EcoTree,
                    BankDetailsId = 3,
                    BillingAddressId = 3,
                    PersonId = 3,
                    VehiculeId = 3,
                    AccountId = 3,
                    EcoWalletId = 3,
                    ShoppingCartId = 3
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 3,
                    Username = "Francois",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 02, 22),
                    LastLoginDate = new DateTime(2022, 05, 06),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 3,
                    BankName = "société Générale",
                    Swift = "FN2365",
                    Iban = "FR7630003035409876543210925"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 3,
                    AddressLine = "3, rue des Alpes",
                    City = "Grenoble",
                    Region = "Isère",
                    Country = "France",
                    PostalCode = 38000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 3,
                    EcoCoinsAmount = 45,
                    Subscription = false,
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 3,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 3,
                    Brand = "Renault",
                    RegistrationNumber = 439872654,
                    Model = "Twingo",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 3,
                    InsuranceId = 3,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 3,
                    InsuranceAgency = "Assurance Car",
                    ContractNumber = "TRA12649"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User4: Houda/random
            this.People.Add(
            new Person
            {
                Id = 4,
                Name = "Houda",
                LastName = "Madi",
                ProfilePicturePath = "/ProfilePictures/ProfileGif19.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 4,
                    Email = "Houda@ecocar.com",
                    PhoneNumber = 0745983265,
                    IdentityCardNumber = 3215769,
                    DrivingPermitNumber = 7395832,
                    BirthDate = new DateTime(1992, 05, 22),
                    UserRating = 4.2,
                    SelectEcoStatusType = User.EcoStatusType.EcoSeed,
                    BankDetailsId = 4,
                    BillingAddressId = 4,
                    PersonId = 4,
                    VehiculeId = 4,
                    AccountId = 4,
                    EcoWalletId = 4,
                    ShoppingCartId = 4
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 4,
                    Username = "Houda",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 03, 05),
                    LastLoginDate = new DateTime(2022, 06, 15),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 4,
                    BankName = "BNP Paribas",
                    Swift = "BNP648",
                    Iban = "FR7630004028379876543210943"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 4,
                    AddressLine = "15, rue de Paris",
                    City = "Paris",
                    Region = "Iles de France",
                    Country = "France",
                    PostalCode = 75000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 4,
                    EcoCoinsAmount = 140,
                    Subscription = false,
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 4,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 4,
                    Brand = "Ford",
                    RegistrationNumber = 439872654,
                    Model = "Fiesta",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 4,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 4,
                    InsuranceAgency = "Eco Mobile",
                    ContractNumber = "Mob5648"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User5: Yannick/random
            this.People.Add(
            new Person
            {
                Id = 5,
                Name = "Yannick",
                LastName = "Sourigues",
                ProfilePicturePath = "/ProfilePictures/ProfileGif3.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 5,
                    Email = "Yannick@ecocar.com",
                    PhoneNumber = 065496328,
                    IdentityCardNumber = 7587190,
                    DrivingPermitNumber = 4532553,
                    BirthDate = new DateTime(1988, 06, 06),
                    UserRating = 3.8,
                    SelectEcoStatusType = User.EcoStatusType.EcoLeaf,
                    BankDetailsId = 5,
                    BillingAddressId = 5,
                    PersonId = 5,
                    VehiculeId = 5,
                    AccountId = 5,
                    EcoWalletId = 5,
                    ShoppingCartId = 5
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 5,
                    Username = "Yannick",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 03, 15),
                    LastLoginDate = new DateTime(2022, 06, 01),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 5,
                    BankName = "HSBC ",
                    Swift = "HSXB5",
                    Iban = "FR1420041010050500013M02606"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 5,
                    AddressLine = "6, rue des Poules",
                    City = "Dijon",
                    Region = "Côte-d'Or",
                    Country = "France",
                    PostalCode = 21000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 5,
                    EcoCoinsAmount = 30,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 06, 20),
                    SubscriptionStart = new DateTime(2022, 05, 20),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 5,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 5,
                    Brand = "Mercedes-Benz",
                    RegistrationNumber = 4303640,
                    Model = "Vito",
                    Hybrid = true,
                    Electric = false,
                    AvailableSeats = 8,
                    InsuranceId = 4,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 5,
                    InsuranceAgency = "Eco Mobile",
                    ContractNumber = "MA26FI"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User6: Benjamin/random
            this.People.Add(
            new Person
            {
                Id = 6,
                Name = "Benjamin",
                LastName = "Dubois",
                ProfilePicturePath = "/ProfilePictures/ProfileGif18.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 6,
                    Email = "Benjamin@ecocar.com",
                    PhoneNumber = 065496328,
                    IdentityCardNumber = 7587190,
                    DrivingPermitNumber = 4532553,
                    BirthDate = new DateTime(1985, 03, 23),
                    UserRating = 4.2,
                    SelectEcoStatusType = User.EcoStatusType.EcoTree,
                    BankDetailsId = 6,
                    BillingAddressId = 6,
                    PersonId = 6,
                    VehiculeId = 6,
                    AccountId = 6,
                    EcoWalletId = 6,
                    ShoppingCartId = 6
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 6,
                    Username = "Benjamin",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2021, 04, 18),
                    LastLoginDate = new DateTime(2022, 05, 16),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 6,
                    BankName = "AXA Banque ",
                    Swift = "A12X32A65",
                    Iban = "FR7612548029989876543210917"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 6,
                    AddressLine = "21, rue des Fleurs",
                    City = "Paris",
                    Region = "Paris",
                    Country = "France",
                    PostalCode = 75000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 6,
                    EcoCoinsAmount = 8,
                    Subscription = false,
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 6,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 6,
                    Brand = "Kia",
                    RegistrationNumber = 9359311,
                    Model = "Picanto",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 6,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 6,
                    InsuranceAgency = "MAAF Assurance",
                    ContractNumber = "26985AE"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User7: Jules/random
            this.People.Add(
            new Person
            {
                Id = 7,
                Name = "Jules",
                LastName = "Bias",
                ProfilePicturePath = "/ProfilePictures/ProfileGif9.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 7,
                    Email = "Jules@ecocar.com",
                    PhoneNumber = 07919140,
                    IdentityCardNumber = 5530882,
                    DrivingPermitNumber = 5218536,
                    BirthDate = new DateTime(1992, 04, 06),
                    UserRating = 4.1,
                    SelectEcoStatusType = User.EcoStatusType.EcoForest,
                    BankDetailsId = 7,
                    BillingAddressId = 7,
                    PersonId = 7,
                    VehiculeId = 7,
                    AccountId = 7,
                    EcoWalletId = 7,
                    ShoppingCartId = 7
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 7,
                    Username = "Jules",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 01, 15),
                    LastLoginDate = new DateTime(2022, 06, 18),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 7,
                    BankName = "Société Générale ",
                    Swift = "HD569UJ",
                    Iban = "FR7630003035409876543210925"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 7,
                    AddressLine = "9, rue Jean Jaurès",
                    City = "Paris",
                    Region = "Paris",
                    Country = "France",
                    PostalCode = 75000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 7,
                    EcoCoinsAmount = 260,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 07, 18),
                    SubscriptionStart = new DateTime(2022, 06, 18),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 7,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 7,
                    Brand = "Fiat",
                    RegistrationNumber = 2537102,
                    Model = "Fiat 500",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 7,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 7,
                    InsuranceAgency = "MAAF Assurance",
                    ContractNumber = "9841TR"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User8: Frédéric/random
            this.People.Add(
            new Person
            {
                Id = 8,
                Name = "Frédéric",
                LastName = "Ferrie",
                ProfilePicturePath = "/ProfilePictures/ProfileGif6.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 8,
                    Email = "Frederic@ecocar.com",
                    PhoneNumber = 06550608,
                    IdentityCardNumber = 8652185,
                    DrivingPermitNumber = 8088763,
                    BirthDate = new DateTime(1984, 06, 06),
                    UserRating = 4,
                    SelectEcoStatusType = User.EcoStatusType.EcoSeed,
                    BankDetailsId = 8,
                    BillingAddressId = 8,
                    PersonId = 8,
                    VehiculeId = 8,
                    AccountId = 8,
                    EcoWalletId = 8,
                    ShoppingCartId = 8
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 8,
                    Username = "Frederic",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2021, 06, 17),
                    LastLoginDate = new DateTime(2022, 06, 18),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 8,
                    BankName = "HSBC",
                    Swift = "Hx909YT",
                    Iban = "FR1420041010050500013M02606"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 8,
                    AddressLine = "9, rue de la mouette",
                    City = "Paris",
                    Region = "Paris",
                    Country = "France",
                    PostalCode = 75000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 8,
                    EcoCoinsAmount = 20,
                    Subscription = false,
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 8,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 8,
                    Brand = "Kia",
                    RegistrationNumber = 8883875,
                    Model = "Sportage",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 8,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 8,
                    InsuranceAgency = "Matmut",
                    ContractNumber = "Mat985"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User9: Hamid/random
            this.People.Add(
            new Person
            {
                Id = 9,
                Name = "Hamid",
                LastName = "Habchi",
                ProfilePicturePath = "/ProfilePictures/ProfileGif4.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 9,
                    Email = "Hamid@ecocar.com",
                    PhoneNumber = 079468690,
                    IdentityCardNumber = 4960679,
                    DrivingPermitNumber = 9902398,
                    UserRating = 3.5,
                    SelectEcoStatusType = User.EcoStatusType.EcoSeed,
                    BankDetailsId = 9,
                    BillingAddressId = 9,
                    PersonId = 9,
                    VehiculeId = 9,
                    AccountId = 9,
                    EcoWalletId = 9,
                    ShoppingCartId = 9
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 9,
                    Username = "Hamid",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 02, 02),
                    LastLoginDate = new DateTime(2022, 06, 01),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 9,
                    BankName = "Banque Postale",
                    Swift = "SP659874",
                    Iban = "FR7612548029989876543210917"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 9,
                    AddressLine = "9, rue du Général",
                    City = "Avignon",
                    Region = "Vaucluse",
                    Country = "France",
                    PostalCode = 84000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 9,
                    EcoCoinsAmount = 0,
                    Subscription = false
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 9,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 9,
                    Brand = "Peugeot",
                    RegistrationNumber = 3793447,
                    Model = "Peugeot 3008",
                    Hybrid = true,
                    Electric = false,
                    AvailableSeats = 5,
                    InsuranceId = 9,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 9,
                    InsuranceAgency = "Macif",
                    ContractNumber = "XOD23658"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User10: Romuald/random
            this.People.Add(
            new Person
            {
                Id = 10,
                Name = "Romuald",
                LastName = "Nguenga",
                ProfilePicturePath = "/ProfilePictures/ProfileGif7.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 10,
                    Email = "Romuald@ecocar.com",
                    PhoneNumber = 07444151,
                    IdentityCardNumber = 5778658,
                    DrivingPermitNumber = 7762147,
                    BirthDate = new DateTime(1978, 03, 15),
                    UserRating = 4.5,
                    SelectEcoStatusType = User.EcoStatusType.EcoTree,
                    BankDetailsId = 10,
                    BillingAddressId = 10,
                    PersonId = 10,
                    VehiculeId = 10,
                    AccountId = 10,
                    EcoWalletId = 10,
                    ShoppingCartId = 10
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 10,
                    Username = "Romuald",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 02, 10),
                    LastLoginDate = new DateTime(2022, 06, 16),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 10,
                    BankName = "AXA Banque",
                    Swift = "OP2698S",
                    Iban = "FR1420041010050500013M02606"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 10,
                    AddressLine = "9, rue des Oiseaux",
                    City = "Bordeaux",
                    Region = "Gironde",
                    Country = "France",
                    PostalCode = 30072
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 10,
                    EcoCoinsAmount = 265,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 10, 20),
                    SubscriptionStart = new DateTime(2022, 04, 20),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 10,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 10,
                    Brand = "Peugeot",
                    RegistrationNumber = 4297647,
                    Model = "Peugeot 408",
                    Hybrid = false,
                    Electric = true,
                    AvailableSeats = 4,
                    InsuranceId = 10,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 10,
                    InsuranceAgency = "Matmut",
                    ContractNumber = "MAT958MUT"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User11: Nelly/random
            this.People.Add(
            new Person
            {
                Id = 11,
                Name = "Nelly",
                LastName = "Ond",
                ProfilePicturePath = "/ProfilePictures/ProfileGif11.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 11,
                    Email = "Nelly@ecocar.com",
                    PhoneNumber = 06865876,
                    IdentityCardNumber = 5140194,
                    DrivingPermitNumber = 9622813,
                    BirthDate = new DateTime(1983, 02, 15),
                    UserRating = 4.3,
                    SelectEcoStatusType = User.EcoStatusType.EcoSeed,
                    BankDetailsId = 11,
                    BillingAddressId = 11,
                    PersonId = 11,
                    VehiculeId = 11,
                    AccountId = 11,
                    EcoWalletId = 11,
                    ShoppingCartId = 11
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 11,
                    Username = "Nelly",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 02, 15),
                    LastLoginDate = new DateTime(2022, 06, 09),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 11,
                    BankName = "BNP Paribas",
                    Swift = "BNP329854",
                    Iban = "FR7630004028379876543210943"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 11,
                    AddressLine = "11, rue de l'Avenue",
                    City = "Paris",
                    Region = "Paris",
                    Country = "France",
                    PostalCode = 75000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 11,
                    EcoCoinsAmount = 10,
                    Subscription = false,
                    EcoCoinsValueEuros = 0
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 11,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 11,
                    Brand = "BMW",
                    RegistrationNumber = 7741973,
                    Model = "X5",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 5,
                    InsuranceId = 11,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 11,
                    InsuranceAgency = "MACIF",
                    ContractNumber = "XY5487PO"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User12: Sabrina/random
            this.People.Add(
            new Person
            {
                Id = 12,
                Name = "Sabrina",
                LastName = "Magana",
                ProfilePicturePath = "/ProfilePictures/ProfileGif12.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 12,
                    Email = "Sabrina@ecocar.com",
                    PhoneNumber = 07325352,
                    IdentityCardNumber = 2138797,
                    DrivingPermitNumber = 6422053,
                    BirthDate = new DateTime(1992, 04, 21),
                    UserRating = 4.4,
                    SelectEcoStatusType = User.EcoStatusType.EcoTree,
                    BankDetailsId = 12,
                    BillingAddressId = 12,
                    PersonId = 12,
                    VehiculeId = 12,
                    AccountId = 12,
                    EcoWalletId = 12,
                    ShoppingCartId = 12
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 12,
                    Username = "Sabrina",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2022, 01, 06),
                    LastLoginDate = new DateTime(2022, 06, 06),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 12,
                    BankName = "AXA Banque",
                    Swift = "3872037XT",
                    Iban = "FR7612548029989876543210917"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 12,
                    AddressLine = "12, rue de Kerichen",
                    City = "Rennes",
                    Region = "Bretagne",
                    Country = "France",
                    PostalCode = 35000
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 12,
                    EcoCoinsAmount = 325,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 07, 05),
                    SubscriptionStart = new DateTime(2022, 03, 05),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 12,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 12,
                    Brand = "BMW",
                    RegistrationNumber = 7741973,
                    Model = "X5",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 5,
                    InsuranceId = 12,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 12,
                    InsuranceAgency = "AssuranceCAR",
                    ContractNumber = "32684512"
                });
            this.SaveChanges();
            #endregion

            #region Initializing User13: Gaïa/random
            this.People.Add(
            new Person
            {
                Id = 13,
                Name = "Gaïa",
                LastName = "Bissé",
                ProfilePicturePath = "/ProfilePictures/ProfileGif15.gif"
            }
            );
            this.Users.Add(
                new User
                {
                    Id = 13,
                    Email = "Gaïa@ecocar.com",
                    PhoneNumber = 06730758,
                    IdentityCardNumber = 7554294,
                    DrivingPermitNumber = 9340798,
                    BirthDate = new DateTime(1992, 04, 04),
                    UserRating = 5,
                    SelectEcoStatusType = User.EcoStatusType.EcoForest,
                    BankDetailsId = 13,
                    BillingAddressId = 13,
                    PersonId = 13,
                    VehiculeId = 13,
                    AccountId = 13,
                    EcoWalletId = 13,
                    ShoppingCartId = 13
                }
                );
            this.Accounts.Add(
                new Account
                {
                    Id = 13,
                    Username = "Gaïa",
                    Password = "5E-A1-36-6C-34-CC-38-0D-AD-F1-62-AC-F6-CB-FD-42", //random
                    CreationDate = new DateTime(2021, 02, 18),
                    LastLoginDate = new DateTime(2022, 06, 18),
                    IsActive = true
                });

            this.BankingDetails.Add(
                new BankDetails
                {
                    Id = 13,
                    BankName = "Société Générale",
                    Swift = "G2A5I7A98",
                    Iban = " FR7630004028379876543210943"
                });
            this.BillingAddresses.Add(
                new BillingAddress
                {
                    Id = 13,
                    AddressLine = "12, rue de Brest",
                    City = "Brest",
                    Region = "Bretagne",
                    Country = "France",
                    PostalCode = 29200
                });
            this.EcoWallets.Add(
                new EcoWallet
                {
                    Id = 13,
                    EcoCoinsAmount = 74,
                    Subscription = true,
                    SubscriptionExpiration = new DateTime(2022, 08, 10),
                    SubscriptionStart = new DateTime(2022, 02, 10),
                });
            this.ShoppingCarts.Add(
                new ShoppingCart
                {
                    Id = 13,
                    QuantityBatchOne = 0,
                    QuantityBatchTwo = 0,
                    QuantityBatchThree = 0,
                    QuantityMonthlySubscription = 0,
                    QuantityTrimestrialSubscription = 0,
                    QuantitySemestrialSubscription = 0,
                    TotalPriceEuros = 0,
                });
            this.Vehicules.Add(
                new Vehicule
                {
                    Id = 13,
                    Brand = "Peugeot",
                    RegistrationNumber = 2751088,
                    Model = "3008",
                    Hybrid = false,
                    Electric = false,
                    AvailableSeats = 4,
                    InsuranceId = 13,
                });
            this.Insurances.Add(
                new Insurance
                {
                    Id = 13,
                    InsuranceAgency = "MAIF",
                    ContractNumber = "87746AEV"
                });
            this.SaveChanges();
            #endregion
        }



        #region Defining character length properties of each table
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // specification on configuration

        //    //Declare non nullable columns
        //    //modelBuilder.Entity<Account>().Property(u => u.Username).IsRequired();
        //    //Add uniqueness constraint
        //    modelBuilder.Entity<Account>().HasIndex(u => u.Username).IsUnique();
        //    modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        //}
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
        #endregion
    }
}