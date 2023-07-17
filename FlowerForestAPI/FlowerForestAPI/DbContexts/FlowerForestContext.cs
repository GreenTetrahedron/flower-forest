using Microsoft.EntityFrameworkCore;
using FlowerForestAPI.Models;

namespace FlowerForestAPI.DbContexts
{
    public class FlowerForestContext : DbContext
    {
        public FlowerForestContext(DbContextOptions<FlowerForestContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username).IsUnique();
        }
    }
}