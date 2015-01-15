namespace Cloud_Vibe.Data.Migrations
{
    using Cloud_Vibe.Data;
    using Cloud_Vibe.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudVibeDbContex>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(CloudVibeDbContex context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            SeedRoles(context);
            SeedAdmin(context);
        }

        //Seed User Roles
        private void SeedRoles(CloudVibeDbContex context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var adminRole = new IdentityRole { Name = "Admin" };
            roleManager.Create(adminRole);

            context.SaveChanges();
        }

        //Seed Admin User
        private void SeedAdmin(CloudVibeDbContex context)
        {
            if (context.Users.Any())
            {
                return;
            }

            var userManager = new UserManager<User>(new UserStore<User>(context));
            var admin = new User()
            {
                UserName = "admin"
            };

            userManager.Create(admin, "admin");
            userManager.AddToRole(admin.Id, "Admin");

            context.SaveChanges();
        }
    }
}
