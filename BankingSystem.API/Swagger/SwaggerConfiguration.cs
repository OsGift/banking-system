using Microsoft.OpenApi.Models;

namespace BankingSystem.API.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankingSystem API", Version = "v1" });
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankingSystem API v1"));
        }
    }
}
