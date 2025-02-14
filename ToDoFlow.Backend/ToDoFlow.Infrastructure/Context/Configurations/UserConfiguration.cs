using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(u => u.Name).HasColumnName("name").HasColumnType("varchar(100)");
            builder.Property(u => u.Email).HasColumnName("email").HasColumnType("varchar(100)");
            builder.Property(u => u.Password).HasColumnName("password_hash").HasColumnType("varchar(60)");
            builder.Property(u => u.CreateAt).HasColumnName("create_at").HasColumnType("datetime");
            builder.Property(u => u.Profile).HasColumnName("profile").HasColumnType("tinyint");

            builder.HasOne(u => u.UserRefreshToken).WithOne(u => u.User).HasForeignKey<UserRefreshToken>(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
