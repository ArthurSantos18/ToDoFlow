using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("id").HasColumnType("int");
            builder.Property(c => c.Name).HasColumnName("name").HasColumnType("varchar(40)");

            builder.Property(t => t.UserId).HasColumnName("user_id");
            builder.HasOne(u => u.User).WithMany(u => u.Categories).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
