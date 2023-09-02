using Microsoft.EntityFrameworkCore;
using CatalogueMicroservice.Models;

namespace CatalogueMicroservice.DbContexts
{
    public class CatalogueDbContext : DbContext
    {
        public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : base(options)
        {
            
        }

        public DbSet<Catalogue> Catalogues { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
