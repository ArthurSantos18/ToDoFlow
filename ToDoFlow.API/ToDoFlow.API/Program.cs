using Microsoft.EntityFrameworkCore;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services;

namespace ToDoFlow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddDbContext<ToDoFlowContext>(options => {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ToDoFlow.Infrastructure"));
            });

            builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
