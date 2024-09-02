using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace theSanju.dataAccess
{
    public class ApplicationRervices : DbContext
    {
        public ApplicationRervices(DbContextOptions<ApplicationRervices> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }

        internal void SaveChangesAsync(EntityEntry<Employee> result)
        {
            throw new NotImplementedException();
        }
    }
}
