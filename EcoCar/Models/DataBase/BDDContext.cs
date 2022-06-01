using EcoCar.Models.FinancialManagement;
using EcoCar.Models.ServiceManagement;
using EcoCar.Models.MessagingManagement;
using Microsoft.EntityFrameworkCore;
using EcoCar.Models.PersonManagement;

namespace EcoCar.Models.DataBase
{
    public class BddContext : DbContext
    {
        //Person Management
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountUser> AccountUser { get; set; }
        public DbSet<AccountAdministrator> AccountAdministrator { get; set; }
        public DbSet<Vehicule> Vehicule { get; set; }
        public DbSet<Insurance> Insurance { get; set; }

        //Financial Management
        public DbSet<EcoWallet> EcoWallet { get; set; }
        public DbSet<BankDetails> BankDetails { get; set; }
        public DbSet<BillingAddress> Billingaddress { get; set; }
        public DbSet<EcoStore> EcoStore { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<ServiceInvoice> ServiceInvoice { get; set; }
        public DbSet<EcoStoreInvoice> EcoStoreInvoice { get; set; }

        //Service Management
        public DbSet<Service> Service { get; set; }
        public DbSet<CarPoolingService> CarPoolingService { get; set; }
        public DbSet<ParcelService> ParcelService { get; set; }
        public DbSet<CarRentalService> CarRentalService { get; set; }
        public DbSet<Trajectory> Trajectory { get; set; }
        public DbSet<Itinerary> Itinerary { get; set; }

        //Messaging Management
        public DbSet<Message> Message { get; set; }
        public DbSet<Reporting> Reporting { get; set; }
        public DbSet<HelpReporting> HelpReporting { get; set; }
        public DbSet<UserReporting> UserReporting { get; set; }
        public DbSet<AdministratorResponse> AdministratorResponse { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=EcoCar");
        }

        public void InitializeDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
            this.Accounts.AddRange(
                new Account
                {
                    Id = 1,
                    Username = "Farah",
                    Password = "Farah",
                    IsActive = true,
                }
                );
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
