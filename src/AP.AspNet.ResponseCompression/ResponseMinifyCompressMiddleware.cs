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
    public class ResponseMinifyCompressMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ResponseMinifyCompressMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
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
            var originalBody = context.Response.Body;
            var bufferStream = new MemoryStream();
            context.Response.Body = bufferStream;

            await _next(context);

            if (!context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.ContentType].ToString().Contains("text/html"))
            {
                return;
            }

            byte[] buf = bufferStream.ToArray();
            byte[] compresedData = Helpers.Compress(buf);
            await originalBody.WriteAsync(compresedData, 0, compresedData.Length);


            context.Response.Body = originalBody;
            bufferStream.Position = 0;

        }
    }
}
