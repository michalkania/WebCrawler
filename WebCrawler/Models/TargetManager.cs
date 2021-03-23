using System;
using System.Collections.Generic;
using WebCrawler.Models;

namespace WebCrawler.Models
{
    /// <summary>
    /// <para>Singleton</para>
    /// Contains the list of targets and its settings. 
    /// </summary>
    /// <remarks>You can add, modify and delete targets in here</remarks>
    public class TargetManager
    {
        public TargetManager Instance { get; }

        #region Priv Properties/Fields

        /// <summary>
        /// Collection of websites to  scrape data from
        /// </summary>
        IDictionary<string, Target> _targets = new Dictionary<string, Target>();

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

        /// <summary>
        /// Adds webstie to the list of targets for data scraping
        /// </summary>
        /// <param name="url">URL to the target website</param>
        /// <param name="name">Name (alias) of the website that you can use to refer to the url later on</param>
        /// <param name="filename">If not null then results will appended to the file</param>
        /// <param name="xpaths">List of XPaths leading to elements containing interesting data to be scraped from the given website</param>
        /// <exception cref="ArgumentNullException">If url is null</exception>
        public void AddTarget(string url, string name = null, string filename = null, IEnumerable<string> xpaths = null)
        {
            name ??= url;
            if (String.IsNullOrEmpty(url)) throw new ArgumentNullException(nameof(url), "Url cannot be null or empty");
            if (!TargetExists(name))
            {
                Target target = new Target(url);
                _targets.Add(name, target);
            }
            else
            {
                throw new ArgumentException(nameof(name), "Target with the given name already exists");
            }
        }

        public void AddTarget(IEnumerable<Target> targets)
        {
            foreach (Target target in targets)
            {
                _targets.Add(target.Name, target);
            }
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
    }
}
