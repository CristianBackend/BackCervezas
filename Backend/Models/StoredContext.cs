using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class StoredContext:DbContext
    {
        public StoredContext(DbContextOptions<StoredContext> options):base(options)
        {
            
        }

        public DbSet<Beer> beers { get; set; }
        public DbSet<Brand> brands { get; set; }
    }
}
