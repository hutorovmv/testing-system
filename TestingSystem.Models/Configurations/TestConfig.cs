using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class TestConfig : EntityTypeConfiguration<Test>
    {
        public TestConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(256);
            Property(p => p.Description).HasColumnType("ntext").IsOptional();
            Property(p => p.TimeRequired).HasColumnType("int").IsOptional();
            Property(p => p.DateTime).HasColumnType("date").IsRequired();

            HasMany(p => p.Questions).WithRequired(p => p.Test).HasForeignKey(p => p.TestId).WillCascadeOnDelete(true);
            HasMany(p => p.TestResults).WithRequired(p => p.Test).HasForeignKey(p => p.TestId).WillCascadeOnDelete(true);
        }
    }
}
