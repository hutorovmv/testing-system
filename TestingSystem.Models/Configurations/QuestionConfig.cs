using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class QuestionConfig : EntityTypeConfiguration<Question>
    {
        public QuestionConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Text).HasColumnType("ntext").IsRequired();

            HasMany(p => p.Answers).WithRequired(p => p.Question).HasForeignKey(p => p.QuestionId).WillCascadeOnDelete(true);
        }
    }
}