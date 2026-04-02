using Microsoft.EntityFrameworkCore;
using ToDoFlow.Infrastructure.Context;

namespace ToDoFlow.API.Extensions
{
    public static class DatabaseCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoFlowContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ToDoFlow.Infrastructure"));
            });

            return services;
        }
    }
}
