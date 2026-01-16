using MetricService.WebAPI.Common;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace MetricService.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddConfiguredSwagger();

            services.AddAsyncInitializer<DbInitializer>();

            return services;
        }

        private static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MetricService",
                    Version = "v1",
                    Description = "A service for working with timescale data results processing"
                });
            });

            return services;
        }
    }
}
