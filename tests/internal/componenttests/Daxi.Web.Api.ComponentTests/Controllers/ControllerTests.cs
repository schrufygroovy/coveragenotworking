using System.Net.Http;
using Daxi.Tests.WebTestHelperLibrary.Mocks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;

namespace Daxi.Web.Api.ComponentTests.Controllers
{
    public abstract class ControllerTests
    {
        private BaseWebApplicationFactory<Startup> factory;

        protected ControllerTests()
        {
            this.factory = new BaseWebApplicationFactory<Startup>();
        }

        protected HttpClient CreateHttpClient()
        {
            return this.GetFactoryWithWebHostBuilder().CreateClient();
        }

        protected HttpMessageInvoker CreateHttpMessageInvoker()
        {
            return new HttpMessageInvoker(
                this.GetFactoryWithWebHostBuilder().Server.CreateHandler(),
                true);
        }

        protected WebApplicationFactory<Startup> GetFactoryWithWebHostBuilder()
        {
            return this.factory.WithWebHostBuilder(
                builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                    });
                });
        }
    }
}
