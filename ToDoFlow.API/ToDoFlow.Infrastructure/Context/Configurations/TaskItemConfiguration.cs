using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.ToTable("task_items");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasColumnName("name").HasColumnType("varchar(60)");
            builder.Property(t => t.Description).HasColumnName("description").HasColumnType("varchar(100)");
            builder.Property(t => t.Status).HasColumnName("status").HasColumnType("tinyint");
            builder.Property(t => t.Priority).HasColumnName("priority").HasColumnType("tinyint");
            builder.Property(t => t.CreatedAt).HasColumnName("created_at").HasColumnType("datetime");
            builder.Property(t => t.CompleteAt).HasColumnName("complete_at").HasColumnType("datetime");

            builder.Property(t => t.CategoryId).HasColumnName("category_id").HasColumnType("int");
            builder.HasOne(c => c.Category).WithMany(c => c.Tasks).HasForeignKey(t => t.CategoryId);

            builder.Property(t => t.UserId).HasColumnName("user_id");
            builder.HasOne(u => u.User).WithMany(u => u.Tasks).HasForeignKey(u => u.UserId);
        }
    }
}
