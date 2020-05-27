namespace MVCPresentationLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using MVCPresentationLayer.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MVCPresentationLayer.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MVCPresentationLayer.Models.ApplicationDbContext";
        }// End Configuration()

        protected override void Seed(MVCPresentationLayer.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            const string admin = "admin@company.com";
            const string adminPassword = "P@ssw0rd";

            // Note: There is purposely no using statment for LogicLayer. This is to force
            // programmers to use the fully qulaified name for our internal UserManager, to 
            // keep it clear that this is not the Identity systesm UserManager class.
            LogicLayer.UserManager userMgr = new LogicLayer.UserManager();
            var roles = userMgr.RetrieveEmployeeRoles();
            foreach (var role in roles)
            {
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = role });
            }
            if (!roles.Contains("Administrator"))
            {
                // Note: even though Administrator should be in the list of roles, this check is to
                // remove any risk of it missing due to deletion from the internal database.
                context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
            }

            if (!context.Users.Any(u => u.UserName == admin))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    GivenName = "Admin",
                    FamilyName = "Company"
                };
                IdentityResult result = userManager.Create(user, adminPassword);
                context.SaveChanges(); // updates the database

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }
            }
        }// End Seed()
    }
}
