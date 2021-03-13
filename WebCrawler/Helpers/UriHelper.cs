using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler.Helpers
{
    public static class UriHelper
    {
        /// <summary>
        /// Checks if a given string is a proper http(s) URL
        /// </summary>
        /// <param name="url">Represents an http(s) address</param>
        /// <returns>True if given string is a correct URL</returns>
        public static bool IsValidUrl(string url)
        {
            if (url == null) throw new ArgumentNullException(nameof(url), "Url cannot be null");
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
