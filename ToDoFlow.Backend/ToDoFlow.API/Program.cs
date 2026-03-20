using ToDoFlow.API.Extensions;
using ToDoFlow.API.Middlewares;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerWithJwt();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddDatabase(builder.Configuration);

            builder.Services.AddRepositories();

            builder.Services.AddServices();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.AddCustomCors(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowLocalHost");

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
