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
        public DbSet<CataloguedPlant> CataloguedPlants { get; set; }
    }
}