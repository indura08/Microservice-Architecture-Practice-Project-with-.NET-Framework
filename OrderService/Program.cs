using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Data.Services;
using System;

namespace OrderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"))
            );

            builder.Services.AddHttpClient<UserClient>();
            builder.Services.AddHttpClient<ProductClient>();
            builder.Services.AddScoped<IOrderService, OrderServiceClass>();
            builder.Services.AddControllers();

            //By default, enums are serialized as their integer values in JSON responses. If you want them to be returned as strings, add this in Program.cs:
            //builder.Services.AddControllers().AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

            //});

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
