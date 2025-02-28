using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Context
{
    public  class ToDoFlowContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<TaskItem> Tasks{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRefreshToken> UserRefreshToken { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.Seed();
        }
    }
}
