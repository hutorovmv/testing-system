using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class TestResultConfig : EntityTypeConfiguration<TestResult>
    {
        public TestResultConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.StartDateTime).HasColumnType("datetime").IsRequired();
            Property(p => p.EndDateTime).HasColumnType("datetime").IsOptional();
            Property(p => p.CorrectAnswers).HasColumnType("int").IsRequired();
        }
    }
}