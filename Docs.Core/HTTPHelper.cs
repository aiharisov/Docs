using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Docs.Core
{
    public class HTTPHelper
    {
        #region Singleton
        private static readonly object padlock = new object();
        private static HTTPHelper _provider;
        public static HTTPHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_provider == null)
                        _provider = new HTTPHelper();
                    return _provider;
                }
            }
        }
        #endregion
        private const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0";
        public string HTTPGet(string URL, Encoding enc)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = enc;
                client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                return client.DownloadString(URL);
            }
        }
        public byte[] HTTPGetFile(string URL, Encoding enc)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = enc;
                client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                return client.DownloadData(URL);
            }
        }
        public string HTTPPost(string URL, Encoding enc, string data)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = enc;
                client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                return client.UploadString(URL, "POST", data);
            }
        }
        public string HTTPPut(string URL, Encoding enc, string data)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = enc;
                client.Headers[HttpRequestHeader.UserAgent] = UserAgent;
                return client.UploadString(URL, "PUT", data);
            }
        }
    }
}
