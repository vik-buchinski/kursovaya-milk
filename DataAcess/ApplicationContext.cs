using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DataAcess.Models;
using DataAcess.Models.Conference;
using DataAcess.Models.ConferenceMember;

namespace DataAcess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserProfile> UserTable { get; set; }
        public DbSet<ConferenceModel> ConferenceTable { get; set; }
        public DbSet<ConferenceCategoryModel> ConferenceCategoryTable { get; set; }
        public DbSet<MemberModel> MembersTable { get; set; }
        public DbSet<MemberWorkModel> MemberWorksTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}