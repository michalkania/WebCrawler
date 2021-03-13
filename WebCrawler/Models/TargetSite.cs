using System;
using System.Collections.Generic;
using WebCrawler.Helpers;

namespace WebCrawle.Models
{
    /// <summary>
    /// Represents target website and scraping configuration for it
    /// </summary>
    public class TargetSite : IEquatable<TargetSite>
    {
        private string _url;

        /// <summary>
        /// Website address as a string
        /// </summary>
        public string Url
        {
            get => _url;
            private set
            {
                if (UriHelper.IsValidUrl(value))
                {
                    Uri.TryCreate(value, UriKind.Absolute, out Uri uri);
                    _url = value;
                    Address = uri;
                }
                else
                {
                    throw new ArgumentException(nameof(value), "URL has a wrong format. Please use e.g. https://google.com");
                }
            }
        }

        /// <summary>
        /// Website address as a <see cref="Uri"/>
        /// </summary>
        public Uri Address { get; private set; }

        /// <summary>
        /// XPath strings that represent where the data is contained
        /// </summary>
        public IList<string> Paths { get; private set; } = new List<string>();

        #region Initialization

        /// <summary>
        /// Represents target website and contains configuration data to be scraped from it
        /// </summary>
        /// <param name="url">Website to get the data from</param>
        /// <exception cref="ArgumentNullException">Url cannot be null</exception>
        public TargetSite(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url), "Url cannot be null");
        }

        /// <summary>
        /// Represents target website and contains configuration data to be scraped from it
        /// </summary>
        /// <param name="url">Website to get the data from</param>
        /// <param name="path">Path that contain data to be scraped</param>
        /// <exception cref="ArgumentNullException">Url cannot be null</exception
        public TargetSite(string url, string path) : this(url)
        {
            if (path == null) throw new ArgumentNullException(nameof(path), "Path cannot be null");
            AddPath(path);
        }

        /// <summary>
        /// Represents target website and contains configuration data to be scraped from it
        /// </summary>
        /// <param name="url">Website to get the data from</param>
        /// <param name="xpathNodes">List of paths that contain data to be scraped</param>
        /// <exception cref="ArgumentNullException">Url cannot be null</exception
        public TargetSite(string url, IEnumerable<string> xpathNodes) : this(url)
        {
            Paths = new List<string>(xpathNodes);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds xpath to the list of nodes to get the data from
        /// </summary>
        /// <param name="xpath">Represents the path to the data</param>
        /// <exception cref="ArgumentNullException">If given path is null</exception>
        /// <exception cref="XPathException">When given path is not a valid xpath</exception>
        public void AddPath(string xpath)
        {
            if (xpath == null) throw new ArgumentNullException(nameof(xpath), "Path cannot be null");
            Paths.Add(xpath);
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares if both <see cref="TargetSite"/> target the same URL
        /// </summary>
        /// <param name="other">Comparing with</param>
        /// <returns>If they target the same URL</returns>
        public bool Equals(TargetSite other)
        {
            if (Url.Equals(other.Url))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Compares if both <see cref="TargetSite"/> target the same URL
        /// </summary>
        /// <param name="other">Comparing with</param>
        /// <returns>If they target the same URL</returns>
        public static bool operator ==(TargetSite left, TargetSite right) => left.Equals(right);
        public static bool operator !=(TargetSite left, TargetSite right) => !left.Equals(right);

        #endregion
    }
}
