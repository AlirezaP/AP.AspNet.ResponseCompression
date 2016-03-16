using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;

namespace AP.AspNet.ResponseCompression
{
    public class ResponseCompressMiddleware
    {
        private System.IO.MemoryStream ms;
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ResponseCompressMiddleware(RequestDelegate next, IHostingEnvironment hostingEnv, ILoggerFactory loggerFactory)
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
            _logger = loggerFactory.CreateLogger<ResponseCompressMiddleware>();
        }

        /// <summary>
        /// Compress response 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {

            if (ms == null)
            {
                ms = new System.IO.MemoryStream();
                context.Response.Body = ms;
            }
            else
            {
                byte[] buf = ms.ToArray();
                byte[] compresedData = Helpers.Compress(buf);


                ms = new System.IO.MemoryStream();
                context.Response.Body.Position = 0;
                context.Response.Body.Write(compresedData, 0, compresedData.Length);
                context.Response.Body = ms;
            }

            return _next(context);
        }
    }
}
