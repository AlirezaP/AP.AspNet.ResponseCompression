using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using System.IO;

namespace AP.AspNet.ResponseCompression
{
    public class ResponseGzipCompressMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ResponseGzipCompressMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (hostingEnv == null)
            {
                throw new ArgumentNullException(nameof(hostingEnv));
            }

            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }


            _next = next;
            _logger = loggerFactory.CreateLogger<ResponseGzipCompressMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Headers[Microsoft.Net.Http.Headers.HeaderNames.AcceptEncoding].ToString().Contains("gzip"))
            {
                await _next(context);
                return;
            }

            var originalBody = context.Response.Body;
            var bufferStream = new MemoryStream();
            context.Response.Body = bufferStream;

            await _next(context);

            context.Response.Body = originalBody;
            bufferStream.Position = 0;

            context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.ContentEncoding] = "gzip";

            using (GZipStream compressionStream = new GZipStream(originalBody, CompressionMode.Compress, false))
            {
                await bufferStream.CopyToAsync(compressionStream);
            }
        }
    }
}
