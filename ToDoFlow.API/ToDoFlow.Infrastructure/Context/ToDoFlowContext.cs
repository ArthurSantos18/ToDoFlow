using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context
{
    public  class ToDoFlowContext : DbContext
    {
        public DbSet<TaskItem> Tasks{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }

        public ToDoFlowContext(DbContextOptions<ToDoFlowContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
