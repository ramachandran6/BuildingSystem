using System.Collections.Generic;
using ES.ElevatorModel;
using Microsoft.EntityFrameworkCore;

namespace ES.ElevatorDataAccess
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options) { }

        public DbSet<PersonDatabase> PersonDet { get; set; }

    }
}