using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace Docs.Core
{
    public static class ZIPHelperExtension
    {
        public static string ZIP(this string data, Encoding encoding)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var compressionStream = new GZipStream(outputStream, CompressionMode.Compress))
                {
                    using (var inputStream = new MemoryStream(encoding.GetBytes(data)))
                    {
                        inputStream.CopyTo(compressionStream);
                    }
                }
                return Convert.ToBase64String(outputStream.ToArray());
            }
        }
        public static string UnZIP(this string data, Encoding encoding)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var inputStream = new MemoryStream(Convert.FromBase64String(data)))
                {
                    using (var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress))
                    {
                        compressionStream.CopyTo(outputStream);
                    }
                }
                return encoding.GetString(outputStream.ToArray());
            }
        }
    }
}
