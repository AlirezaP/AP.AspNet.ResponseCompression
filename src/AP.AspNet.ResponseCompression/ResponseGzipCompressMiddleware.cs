using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;

namespace AP.AspNet.ResponseCompression
{
    public class ResponseGzipCompressMiddleware
    {
        private System.IO.MemoryStream ms;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly CompressionOption _option;

        public ResponseGzipCompressMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, CompressionOption option, ILoggerFactory loggerFactory)
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
            _option = option;
            _logger = loggerFactory.CreateLogger<ResponseGzipCompressMiddleware>();
        }

        /// <summary>
        /// Compress response 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
        public Task Invoke(HttpContext context)
        {
            if (ms == null)
            {
                ms = new System.IO.MemoryStream();
                context.Response.Body = ms;
            }

            byte[] buf = ms.ToArray();

            if (buf.Length > 0)
            {
                byte[] compresedData;

                if (_option.EnabledMinification)
                {
                    buf = Helpers.Compress(buf);
                }

                using (System.IO.MemoryStream msGzip = new System.IO.MemoryStream())
                {
                    using (GZipStream compressionStream = new GZipStream(msGzip,
                       CompressionMode.Compress, false))
                    {
                        compressionStream.Write(buf, 0, buf.Length);
                    }

                    compresedData = msGzip.ToArray();

                    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.ContentEncoding] = "gzip";

                    ms = new System.IO.MemoryStream();
                    context.Response.Body.Position = 0;
                    context.Response.Body.Write(compresedData, 0, compresedData.Length);
                    context.Response.Body = ms;
                }
            }
            return _next(context);
        }
    }
}
