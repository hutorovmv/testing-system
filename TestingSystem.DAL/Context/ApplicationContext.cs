using static System.Data.Entity.SqlServer.SqlProviderServices;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TestingSystem.Models.Entities;
using TestingSystem.Models.Configurations;

namespace TestingSystem.DAL.Context
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestAnswer> TestAnswers { get; set; }

        public ApplicationContext() { }

        public ApplicationContext(string connectionString) : base(connectionString) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserProfileConfig());
            modelBuilder.Configurations.Add(new TestConfig());
            modelBuilder.Configurations.Add(new QuestionConfig());
            modelBuilder.Configurations.Add(new AnswerConfig());
            modelBuilder.Configurations.Add(new TestResultConfig());
            modelBuilder.Configurations.Add(new TestAnswerConfig());
            base.OnModelCreating(modelBuilder);
        }

        public void SetModified<T>(T item) => Entry(item).State = EntityState.Modified;
    }
}
