using Crm.Domains.Data;
using DNI.Core.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Data
{
    public class CrmDbContext : DbContextBase
    {
        public CrmDbContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions, true, true, true)
        {
        }
        public DbSet<Hash> Hashes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Domains.Data.Attribute> Attributes { get; set; }
        public DbSet<CustomerAttribute> CustomerAttributes { get; set; }
        public DbSet<CustomerHash> CustomerHashes { get; set; }

        protected override void ModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hash>().HasNoKey();
            base.ModelCreating(modelBuilder);
        }

    }
}
