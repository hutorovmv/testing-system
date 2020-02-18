using System.Data.Entity.ModelConfiguration;
using TestingSystem.Models.Entities;

namespace TestingSystem.Models.Configurations
{
    public class UserProfileConfig : EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfig()
        {
            HasKey(p => p.Id);
            
            Property(p => p.FirstName).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.LastName).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(p => p.ProfilePhoto).HasColumnType("image").IsOptional();
            Property(p => p.BirthDate).HasColumnType("date").IsOptional();
            Property(p => p.ContactEmail).HasColumnType("nvarchar").HasMaxLength(256);
            Property(p => p.ContactPhone).HasColumnType("nvarchar").HasMaxLength(15);
            Property(p => p.AboutMe).HasColumnType("ntext").IsOptional();

            HasRequired(p => p.ApplicationUser).WithRequiredDependent(p => p.UserProfile).WillCascadeOnDelete(true);
            HasMany(p => p.Tests).WithRequired(p => p.Author).HasForeignKey(p => p.AuthorId).WillCascadeOnDelete(false);
            HasMany(p => p.TestResults).WithOptional(p => p.User).HasForeignKey(p => p.UserId).WillCascadeOnDelete(true);  
        }
    }
}
