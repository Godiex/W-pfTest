using Domain.Entities;
using System.Data.Entity;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>()
                .HasKey(a => a.Identification); 

            modelBuilder.Entity<User>()
                .HasKey(u => u.Identification); 
            base.OnModelCreating(modelBuilder);
        }
    }
}
