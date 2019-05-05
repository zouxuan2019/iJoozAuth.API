using iJoozAuth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace iJoozAuth.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<ApplicationUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}