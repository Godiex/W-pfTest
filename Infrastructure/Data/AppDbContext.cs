
using Domain.Entities;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
    }
}
