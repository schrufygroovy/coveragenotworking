using Daxi.Web.Api.Shared.Configurations;
using Daxi.Web.Api.Shared.Conventions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Converters;

namespace Daxi.Web.Api.Shared.Extensions
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder ConfigureSwaggerApiConvention(
            this IMvcBuilder mvcBuilder,
            ApiSettings apiSettings)
        {
            return mvcBuilder
                .AddMvcOptions(
                    c => c.Conventions.Add(new ApiExplorerConvention(apiSettings)));
        }

        public static IMvcBuilder AddNewtonsoftJsonWithStringEnumConverter(
            this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddNewtonsoftJson(options =>
                options.SerializerSettings.Converters.Add(new StringEnumConverter()));
        }
    }
}
