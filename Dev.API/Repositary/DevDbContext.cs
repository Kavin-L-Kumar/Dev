using Dev.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dev.API.Repositary
{
    public class DevDbContext: DbContext
    {
        public DevDbContext(DbContextOptions dbContextOptions) :base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
