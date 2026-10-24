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


    }
}
