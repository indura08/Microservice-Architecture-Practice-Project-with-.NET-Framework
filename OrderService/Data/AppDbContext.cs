using Microsoft.EntityFrameworkCore;
using OrderService.Model;

namespace OrderService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}

        public DbSet<Order> Orders { get; set; }
    }
}
