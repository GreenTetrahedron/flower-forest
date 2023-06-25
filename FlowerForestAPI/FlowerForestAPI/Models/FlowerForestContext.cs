using Microsoft.EntityFrameworkCore;

namespace FlowerForestAPI.Models
{
    public class FlowerForestContext : DbContext
    {
        public FlowerForestContext(DbContextOptions<FlowerForestContext> options)
            : base(options)
        {
        }

        public DbSet<Plant> Plants { get; set; }
    }
}