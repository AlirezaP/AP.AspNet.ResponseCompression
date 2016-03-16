using System.Text.RegularExpressions;

namespace AP.AspNet.ResponseCompression
{
    internal static class Helpers
    {
        internal static byte[] Compress(byte[] data)
        {
            string txt = System.Text.Encoding.UTF8.GetString(data, 0, data.Length);
            string pattern = @"\n+";
            Regex r = new Regex(pattern);
            string compressed = r.Replace(txt, "");

            pattern = @"\s+";
            r = new Regex(pattern);
            compressed = r.Replace(compressed, " ");

            return System.Text.Encoding.UTF8.GetBytes(compressed);

        }
    }
}
