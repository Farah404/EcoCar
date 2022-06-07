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
