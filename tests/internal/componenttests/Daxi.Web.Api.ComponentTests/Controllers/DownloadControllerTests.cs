using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Daxi.Web.Api.ComponentTests.Controllers
{
    public class DownloadControllerTests : ControllerTests
    {
        [Test]
        public async Task DownloadRikimaru_ShouldProvideJsonFile()
        {
            var rikimaru = new Rikimaru
            {
                Id = new Guid("4CA01206-BE49-4977-A3FE-AF83144F6195")
            };
            var client = this.CreateHttpClient();
            var response = await client.GetAsync($"api/download/rikimaru?rikimaruId={rikimaru.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(
                response.StatusCode,
                Is.EqualTo(HttpStatusCode.OK),
                $"Response was: {responseString}.");

            Assert.That(
                response.Content.Headers.ContentDisposition.FileName,
                Is.EqualTo("4ca01206-be49-4977-a3fe-af83144f6195.json"));
        }

        [Test]
        public async Task DownloadRikimarus_MultipleWikifolios_ShouldUseTimeForFileName()
        {
            var rikimaru = new Rikimaru
            {
                Id = new Guid("4CA01206-BE49-4977-A3FE-AF83144F6195")
            };
            var rikimaru2 = new Rikimaru
            {
                Id = new Guid("4CA01206-BE49-4977-A3FE-AF83144F6196")
            };
            var client = this.CreateHttpClient();
            var response = await client.GetAsync($"api/download/rikimarus?rikimaruIds={rikimaru.Id}&rikimaruIds={rikimaru2.Id}");
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.That(
                response.StatusCode,
                Is.EqualTo(HttpStatusCode.OK),
                $"Response was: {responseString}.");

            Assert.That(
                response.Content.Headers.ContentDisposition.FileName,
                Does.Match("20.+\\.zip"));
        }
    }
}
