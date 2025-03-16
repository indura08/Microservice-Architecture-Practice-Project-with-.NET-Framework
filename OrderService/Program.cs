using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Data.Services;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConncetionString"))
            );

            builder.Services.AddHttpClient<UserClient>();
            builder.Services.AddHttpClient<ProductClient>();
            builder.Services.AddScoped<IOrderService, OrderServiceClass>();
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

            app.Run();
        }
    }
}
