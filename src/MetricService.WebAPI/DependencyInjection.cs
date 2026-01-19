using Mapster;
using MetricService.Application.Common.Mappings;
using MetricService.WebAPI.Common;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MetricService.WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddConfiguredSwagger();

            services.AddConfiguredMapster();

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

        private static void AddConfiguredMapster(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ResultMappingConfiguration).Assembly;
            var presentationAssembly = typeof(ResultRequestsMappingConfig).Assembly;

            var configuration = TypeAdapterConfig.GlobalSettings;
            configuration.Scan(applicationAssembly, presentationAssembly);

            services.AddSingleton(configuration);
            services.AddMapster();
        }
    }
}
