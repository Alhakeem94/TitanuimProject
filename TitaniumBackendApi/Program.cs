
using Microsoft.EntityFrameworkCore;
using TitaniumBackendApi.Data;
using TitaniumBackendApi.Interfaces;
namespace TitaniumBackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<AmanaDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) 
            );
            builder.Services.AddScoped<ICustomers, CustomersRepository>();




            var app = builder.Build();







            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
