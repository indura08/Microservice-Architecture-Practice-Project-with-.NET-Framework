
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductService.Data;
using ProductService.Data.Services;

namespace ProductService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //configuring mongodb settings
            builder.Services.Configure<MongoDBSettings>(
                builder.Configuration.GetSection("MongoDB"));

            //Register mongodb client
            builder.Services.AddSingleton<IMongoClient>(s =>
                new MongoClient(builder.Configuration.GetValue<string>("MongoDB:ConnectionString")));
                //new MongoClient(builder.Configuration.GetSection("MongoDB")["ConnectionString"])
                //mehemath puluwan
    
            builder.Services.AddSingleton(s =>
            {
                var settings = s.GetRequiredService<IOptions<MongoDBSettings>>().Value;
                var client = s.GetRequiredService<IMongoClient>();
                var database = client.GetDatabase(settings.DatabaseName);
                return database;
            });
            builder.Services.AddScoped<IProductService, ProductServiceClass>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapGet("/", () => "Product service is running no issue found");
            app.Run();
        }
    }
}
