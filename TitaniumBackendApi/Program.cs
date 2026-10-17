
namespace TitaniumBackendApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


             //Microsot2



            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.MapOpenApi();
            //}
            //this is the diffrent
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            Console.WriteLine(" this is first API project");

            app.Run();
        }
    }
}
