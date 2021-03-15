using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using WebCrawle.Models;

namespace WebCrawler
{
    /// <summary>
    /// Downloads and strips data
    /// </summary>
    public class Crawler
    {
        #region Pub Properties

        /// <summary>
        /// Crawler's behavior
        /// </summary>
        public CrawlerSettings Settings { get; private set; }


        #endregion

        #region Priv Properties/Fields

        /// <summary>
        /// Collection of websites to  scrape data from
        /// </summary>
        IDictionary<string, Target> _targets = new Dictionary<string, Target>();

        #endregion

        #region Initialization

        public Crawler() : this(null) { }

        public Crawler(CrawlerSettings settings)
        {
            Settings = settings ??= DefaultSettings();
        }

        private CrawlerSettings DefaultSettings()
        {
            return new CrawlerSettings(RunMode.AllLists, TimeSpan.Zero);
        }

        #endregion

        #region Priv Methods

        /// <summary>
        /// Checks if there is already a target with this name
        /// </summary>
        /// <param name="key">Alias or url of the website</param>
        /// <returns>Is target in the collection?</returns>
        private bool TargetExists(string key)
        {
            return _targets.ContainsKey(key);
        }

        /// <summary>
        /// Returns <see cref="Target"/> under given name (key)
        /// </summary>
        /// <param name="key">Is a name or an alias for the website</param>
        /// <exception cref="KeyNotFoundException">If the key (name or url) is not in the collection</exception>
        private Target GetTarget(string key)
        {
            if (_targets.TryGetValue(key, out Target target))
            {
                return target;
            }
            else
            {
                throw new KeyNotFoundException($"Key has not been found in the collection of Targets for the key '{key}'");
            }
        }

        #endregion

        #region API

        /// <summary>
        /// Adds website to the list of targets for data scraping. 
        /// </summary>
        /// <param name="url">Website http(s) address</param>
        /// <param name="webName">Alias name for the website</param>
        /// <exception cref="ArgumentNullException">If url is null</exception>
        /// <exception cref="ArgumentException">If webName is already in the dictionary of targets</exception>
        public void AddTarget(string url, string webName = null, string xpath = null)
        {
            webName ??= url;
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url cannot be null or empty");
            if (!TargetExists(webName))
            {
                Target target = new Target(url);
                _targets.Add(webName, target);
            }
            else
            {
                throw new ArgumentException(nameof(webName), "Target with the given name already exists");
            }
        }

        /// <summary>
        /// Adds webstie to the list of targets for data scraping
        /// </summary>
        /// <param name="webName">Name of the website that you can use to refer to the url later on</param>
        /// <param name="url">URL to the target website</param>
        /// <param name="xpaths">List of XPaths leading to elements containing interesting data to be scraped from the given website</param>
        /// <exception cref="ArgumentNullException">If url is null</exception>
        public void AddTarget(string webName, string url, IEnumerable<string> xpaths)
        {
            webName ??= url;
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url cannot be null or emtpy");

            Target target = new Target(url, xpaths);

        }

        /// <summary>
        /// Adds xpath to url
        /// </summary>
        /// <param name="name">Name representing an alias or url of a website address </param>
        /// <param name="xpath">Xpath to the node containing interesting data</param>
        /// <exception cref="KeyNotFoundException">If there is no target with the given name that is an alias or an url address</exception>
        public void AddPath(string name, string xpath)
        {
            Target target = GetTarget(name);
            target.AddPath(xpath);
        }

        /// <summary>
        /// Adds xpath to url
        /// </summary>
        /// <param name="name">URL address of the website to scrape the data from</param>
        /// <param name="xpaths">Xpaths to the nodes containing interesting data</param>
        /// <exception cref="KeyNotFoundException">If there is no target with the given name that is an alias or an url address</exception>
        public void AddPath(string name, IEnumerable<string> xpaths)
        {
            Target target = GetTarget(name);
            foreach (string path in xpaths)
            {
                AddPath(name, path);
            }
        }

        /// <summary>
        /// Returns all alias/url keys representing the target websites
        /// </summary>
        public IEnumerable<string> GetTargetNames()
        {
            return _targets.Keys;
        }

        #endregion
    }
}
