using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IPT102_PALINO_SYSTEM.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        // Other DbSet properties as needed

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure your model relationships here if needed

            base.OnModelCreating(modelBuilder);
        }
    }
}