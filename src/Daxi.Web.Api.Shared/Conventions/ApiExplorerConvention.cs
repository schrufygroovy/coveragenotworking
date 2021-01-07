using System.Linq;
using Daxi.Web.Api.Shared.Configurations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Daxi.Web.Api.Shared.Conventions
{
    public class ApiExplorerConvention : IActionModelConvention
    {
        public ApiExplorerConvention(ApiSettings settings)
        {
            this.Settings = settings;
        }

        private ApiSettings Settings { get; set; }

        public void Apply(ActionModel action)
        {
            if (this.Settings?.AllowPost == false
                && action.Attributes.OfType<HttpPostAttribute>().Any())
            {
                action.ApiExplorer.IsVisible = false;
            }

            if (this.Settings?.AllowDelete == false
                && action.Attributes.OfType<HttpDeleteAttribute>().Any())
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }
}
