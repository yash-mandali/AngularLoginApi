using AngularLoginApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AngularLoginApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; } 
    }
}
