using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class AnswerConfig : EntityTypeConfiguration<Answer>
    {
        public AnswerConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Text).HasColumnType("ntext").IsRequired().HasMaxLength(256);
            Property(p => p.IsCorrect).HasColumnType("bit").IsRequired();
        }
    }
}

