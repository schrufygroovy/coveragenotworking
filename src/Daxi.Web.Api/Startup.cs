using System;
using System.IO;
using Daxi.Web.Api.Controllers;
using Daxi.Web.Api.Shared.Configurations;
using Daxi.Web.Api.Shared.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Daxi.Web.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ApplicationInfo = this.GetApplicationInfo();
        }

        public IConfiguration Configuration { get; }

        private OpenApiInfo ApplicationInfo { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJsonWithStringEnumConverter()
                .ConfigureSwaggerApiConvention(this.GetApiSettings());
            services
                .ConfigureLowercaseUrlsOnly()
                .ConfigureSwagger(this.ApplicationInfo, GetXmlDocumentationFilePaths());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomExceptionHandler();

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseCustomSwagger(this.ApplicationInfo);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        private static string GetFullXmlDocumentationFilePath(string xmlFileName)
        {
            return $"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{xmlFileName}";
        }

        private static string[] GetXmlDocumentationFilePaths()
        {
            // XML documents must be generated via .csproj configuration
            // see https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.1&tabs=visual-studio#xml-comments
            return new[]
            {
                GetFullXmlDocumentationFilePath($"{typeof(DownloadController).Assembly.GetName().Name}.xml")
            };
        }

        private ApiSettings GetApiSettings()
        {
            return this.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
        }

        private OpenApiInfo GetApplicationInfo()
        {
            return new OpenApiInfo
            {
                Title = $"{nameof(Daxi)}.{nameof(Web)}.{nameof(Api)} - {this.GetApiSettings().EnvironmentName}",
                Version = "v1",
                Contact = new OpenApiContact
                {
                    Email = "david.schruf@wikifolio.com"
                },
                Description = "qwerqwer"
            };
        }
    }
}
