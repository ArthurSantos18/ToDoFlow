using Microsoft.OpenApi.Models;

namespace ToDoFlow.API.Extensions
{
    public static class SwaggerCollectionExtensions
    {
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(
               c =>
               {
                   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                   {
                       Description = "Exemplo: 'bearer {token}'",
                       Type = SecuritySchemeType.ApiKey,
                       Name = "Authorization",
                       In = ParameterLocation.Header
                   });

                   c.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                   });
               });

            return services;
        }
    }
}
