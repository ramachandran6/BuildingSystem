using System.Collections.Generic;
using ES.BS.Model;
using Microsoft.EntityFrameworkCore;

namespace ES.BS.DataAccessLayer
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<PersonDatabase> PersonDet { get; set; }
        public DbSet<BuildingSystem> BuildingSystemss { get; set; }
        public DbSet<WorkerDetails> workerDetailss { get; set; }

    }
    
}