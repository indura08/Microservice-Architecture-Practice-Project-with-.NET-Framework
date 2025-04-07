using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Security.Claims;
using System.Text;
using UserService.Data;
using UserService.Data.Services;
using UserService.Model;
using UserService.Repository;

namespace UserService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConncetionString"))
            );
            // Add services to the container.
            builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager()
                .AddRoles<IdentityRole>();

            //jwt configurations
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Add this line for local testing
                options.SaveToken = true;
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        var exception = context.Exception;
                        Console.WriteLine($"Token validation failed 44: {exception.Message}");
                        return Task.CompletedTask;
                    }
                };

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped<IUserService, UserServiceClass>();
            builder.Services.AddScoped<IUserAccount, AccountRepository>();

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            IdentityModelEventSource.ShowPII = true;

            var logger = new LoggerConfiguration().WriteTo.Console()
                                                  .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                                                  {
                                                      AutoRegisterTemplate = true,
                                                      IndexFormat = "user-service-logs-{0:yyyy.MM.dd}"
                                                  })
                                                  .Enrich.FromLogContext()
                                                  .CreateLogger();
            builder.Logging.AddSerilog(logger);
            builder.Host.UseSerilog();                                  

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            //me auhtorization eki authentication eki ena piliwela me widiyam wenna one nattnm rolebase access hariyt sidda wenne nh
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
