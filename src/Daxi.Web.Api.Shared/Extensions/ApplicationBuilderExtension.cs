using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace Daxi.Web.Api.Shared.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseCustomSwagger(this IApplicationBuilder app, OpenApiInfo applicationInfo)
        {
            var version = applicationInfo.Version;
            // To customize the UI
            app.UseStaticFiles();
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{applicationInfo.Title} {version}");
                // To serve the Swagger UI at the apps root
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = applicationInfo.Title;
            });
        }

        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler("/error")
                .UseMiddleware<Middleware.CancellationHandlingMiddleware>();
        }
    }
}
