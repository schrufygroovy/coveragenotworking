using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Daxi.Web.Api.Shared.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureLowercaseUrlsOnly(this IServiceCollection services)
        {
            return services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
        }

        public static IServiceCollection ConfigureSwagger(
            this IServiceCollection services,
            OpenApiInfo applicationInfo,
            params string[] xmlDocumentationFiles)
        {
            return services.AddSwaggerGen(
                swaggerGenOptions =>
                {
                    swaggerGenOptions.SwaggerDoc(
                        applicationInfo.Version,
                        applicationInfo);
                    IncludeXmlComments(swaggerGenOptions, xmlDocumentationFiles);
                });
        }

        public static IServiceCollection ConfigureAsSubSection<T>(this IServiceCollection serviceCollection, IConfiguration configuration)
            where T : class
        {
            return serviceCollection.ConfigureSection<T>(configuration, typeof(T).Name);
        }

        public static IServiceCollection ConfigureSection<TSectionType>(this IServiceCollection serviceCollection, IConfiguration configuration, string sectionName)
            where TSectionType : class
        {
            return serviceCollection.Configure<TSectionType>(configuration.GetSection(sectionName));
        }

        private static void IncludeXmlComments(SwaggerGenOptions swaggerGenOptions, string[] xmlDocumentationFiles)
        {
            if (xmlDocumentationFiles == null)
            {
                return;
            }

            foreach (var xmlDocumentationFile in xmlDocumentationFiles)
            {
                if (!string.IsNullOrEmpty(xmlDocumentationFile))
                {
                    swaggerGenOptions.IncludeXmlComments(xmlDocumentationFile);
                }
            }
        }
    }
}
