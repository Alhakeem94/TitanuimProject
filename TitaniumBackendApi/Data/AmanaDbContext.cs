using Microsoft.EntityFrameworkCore;
using Shared.Models;



namespace TitaniumBackendApi.Data
{
    public class AmanaDbContext : DbContext
    {
        public AmanaDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<CustomersModel> CustomersTable { get; set; }
        public DbSet<Account> AccountsTable { get; set; }





        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configuration.CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new Configuration.AccountConfiguration());

        }



    }
}
