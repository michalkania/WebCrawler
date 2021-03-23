using System;
using System.Collections.Generic;
using WebCrawler.Helpers;

namespace WebCrawler.Models
{
    /// <summary>
    /// Represents a targeted website configuration and parameters
    /// </summary>
    public class Target : IEquatable<Target>
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
        /// Alias name for the target
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// If not null. Then the results will be appended to the file with this file name.
        /// </summary>
        public string FileName { get; set; }

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
        public Target(string url)
        {
            Url = url ?? throw new ArgumentNullException(nameof(url), "Url cannot be null");
        }

        /// <summary>
        /// Represents target website and contains configuration data to be scraped from it
        /// </summary>
        /// <param name="url">Website to get the data from</param>
        /// <param name="filename">Name of the file to which results should be appended</param>
        /// <exception cref="ArgumentNullException">Url cannot be null</exception>
        public Target(string url, string filename = null) : this(url)
        {
            FileName = filename;
        }

        /// <summary>
        /// Represents target website and contains configuration data to be scraped from it
        /// </summary>
        /// <param name="url">Website to get the data from</param>
        /// <param name="xpathNodes">List of paths that contain data to be scraped</param>
        /// <exception cref="ArgumentNullException">Url cannot be null</exception
        public Target(string url, IEnumerable<string> xpathNodes) : this(url)
        {
            Paths = new List<string>(xpathNodes);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds xpath to the list of nodes to get the data from
        /// </summary>
        /// <param name="xpath">Represents the path to the data</param>
        public void AddPath(string xpath)
        {
            if (!String.IsNullOrEmpty(xpath))
            {
                Paths.Add(xpath);
            }
        }

        /// <summary>
        /// Adds xpaths to the list of nodes to get the data from
        /// </summary>
        /// <param name="xpaths">Represents a list of paths to the data</param>
        /// <exception cref="ArgumentNullException">If path in the list is null</exception>
        public void AddPath(IEnumerable<string> xpaths)
        {
            foreach (string item in xpaths)
            {
                if (!String.IsNullOrEmpty(item))
                {
                    Paths.Add(item);
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares if both <see cref="Target"/> target the same URL
        /// </summary>
        /// <param name="other">Comparing with</param>
        /// <returns>If they target the same URL</returns>
        public bool Equals(Target other)
        {
            if (Url.Equals(other.Url))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Compares if both <see cref="Target"/> target the same URL
        /// </summary>
        /// <param name="other">Comparing with</param>
        /// <returns>If they target the same URL</returns>
        public static bool operator ==(Target left, Target right) => left.Equals(right);
        public static bool operator !=(Target left, Target right) => !left.Equals(right);

        #endregion
    }
}
