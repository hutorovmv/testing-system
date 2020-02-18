using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class TestAnswerConfig : EntityTypeConfiguration<TestAnswer>
    {
        public TestAnswerConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.IsCorrect).HasColumnType("bit").IsRequired();

            HasRequired(p => p.TestResult).WithMany(p => p.TestAnswers).HasForeignKey(p => p.TestResultId).WillCascadeOnDelete(false);
            HasRequired(p => p.Question).WithMany(p => p.TestAnswers).HasForeignKey(p => p.QuestionId).WillCascadeOnDelete(false);

            HasMany(p => p.Answers).WithMany(p => p.TestAnswers).Map(m => {
                m.ToTable("AnswerTestAnswer");
                m.MapLeftKey("TestAnswerId");
                m.MapRightKey("AnswerId");
            });
        }
    }
}
