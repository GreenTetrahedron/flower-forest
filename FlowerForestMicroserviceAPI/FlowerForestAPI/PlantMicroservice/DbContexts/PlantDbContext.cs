using Microsoft.EntityFrameworkCore;
using PlantMicroservice.Models;

namespace PlantMicroservice.DbContexts
{
    public class PlantDbContext : DbContext
    {
        public PlantDbContext(DbContextOptions<PlantDbContext> options) : base(options)
        {
            
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
    }
}
