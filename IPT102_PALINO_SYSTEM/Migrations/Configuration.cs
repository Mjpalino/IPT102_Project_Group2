namespace IPT102_PALINO_SYSTEM.Migrations
{
    using IPT102_PALINO_SYSTEM.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IPT102_PALINO_SYSTEM.Models.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            context.Roles.AddOrUpdate(r => r.Name,
            new Role { Name = "Administrator" },
            new Role { Name = "MedicalStaff" },
            new Role { Name = "Patient" }
    );
        }
    }
}
