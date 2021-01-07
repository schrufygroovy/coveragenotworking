using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading;
using Daxi.Libraries.MemoryStreamer;
using Microsoft.AspNetCore.Mvc;

namespace Daxi.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        /// <summary>
        /// Download a rikimaru to single json file.
        /// </summary>
        [HttpGet("rikimaru")]
        public IActionResult DownloadRikimaru(
            CancellationToken cancellationToken,
            [FromQuery] Guid rikimaruId,
            bool formattingIndented = true)
        {
            var dataSet = new Rikimaru
            {
                Id = rikimaruId
            };

            var memoryStream = new JsonStreamer().GetMemoryStream(dataSet, formattingIndented);

            return this.File(
                memoryStream,
                MediaTypeNames.Application.Json,
                $"{rikimaruId}.json");
        }

        [HttpGet("rikimarus")]
        public IActionResult DownloadRikimarus(
            CancellationToken cancellationToken,
            [FromQuery] Guid[] rikimaruIds,
            bool formattingIndented = true)
        {
            var resultFiles = new Dictionary<string, Stream>();

            foreach (var rikimaruId in rikimaruIds)
            {
                var dataSet = new Rikimaru
                {
                    Id = rikimaruId
                };
                resultFiles.Add(
                    $"{rikimaruId}.json", new JsonStreamer().GetMemoryStream(dataSet, formattingIndented));
            }

            var memoryStream = new ZipStreamer().GetZipStream(resultFiles);

            memoryStream.Position = 0;
            return this.File(
                memoryStream,
                MediaTypeNames.Application.Zip,
                $"{DateTime.UtcNow:yyyyMMdd-HHmmss}.zip");
        }
    }
}
