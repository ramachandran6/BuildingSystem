
using ES.BS.Model;
using Microsoft.EntityFrameworkCore;

namespace ES.BS.DataAccessLayer
{
    public class BuildingDbContext:DbContext
    {
        public static readonly object BuildingSystems;

        public BuildingDbContext(DbContextOptions<BuildingDbContext> options) : base(options) { }

        public DbSet<BuildingSystem> buildingSystems { get; set; }
    }
}