using System;
using Microsoft.AspNet.Http;
using AP.AspNet.ResponseCompression;

namespace Microsoft.AspNet.Builder
{
    public static class ResponseCompressExtensions
    {
        /// <summary>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGzipResponseCompress(this IApplicationBuilder builder, CompressionOption options)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ResponseGzipCompressMiddleware>(options);
        }
    }
}
