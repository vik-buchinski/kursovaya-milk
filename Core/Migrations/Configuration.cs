using DataAcess;
using WebMatrix.WebData;
using System.Web.Security;
using System.Data.Entity.Migrations;

namespace Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationContext context)
        {
            context.UserTable.Find(1);
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("ApplicationContext", "UserProfile", "UserId", "Email", autoCreateTables: true);
            }
            foreach (string role in Constants.ROLES_LIST)
            {
                if (!Roles.RoleExists(role))
                {
                    Roles.CreateRole(role);
                }
            }
        }
    }
}
