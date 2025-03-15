using Microsoft.EntityFrameworkCore;
using UserService.Model;

namespace UserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {}
        public DbSet<User> Users { get; set;}

    }
}
