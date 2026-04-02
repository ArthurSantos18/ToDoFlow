using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Application.Services.Utils;
using ToDoFlow.Application.Services;

namespace ToDoFlow.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<IEnumService, EnumService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IBCryptPasswordService, BCryptPasswordService>();

            return services;
        }
    }
}
