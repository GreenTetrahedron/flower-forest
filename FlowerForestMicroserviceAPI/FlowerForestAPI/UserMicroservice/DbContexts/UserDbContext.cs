using Microsoft.EntityFrameworkCore;
using UserMicroservice.Models;

namespace UserMicroservice.DbContexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
    }
}
