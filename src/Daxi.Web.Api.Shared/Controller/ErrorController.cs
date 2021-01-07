using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Daxi.Web.Api.Shared.Controller
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            return this.Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
