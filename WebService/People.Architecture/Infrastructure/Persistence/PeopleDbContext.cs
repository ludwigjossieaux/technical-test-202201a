using Microsoft.EntityFrameworkCore;
using People.Architecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Architecture.Infrastructure.Persistence
{
    [ExcludeFromCodeCoverage]
    public class PeopleDbContext : DbContext
    {
        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
    }
}
