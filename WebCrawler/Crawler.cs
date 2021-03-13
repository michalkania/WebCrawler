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
        Dictionary<string, TargetSite> _targets = new Dictionary<string, TargetSite>();

        readonly ILogger _logger;

        #endregion

        #region Initialization

        public Crawler() : this(NullLoggerFactory.Instance) { }

        public Crawler(ILoggerFactory log) : this(log, null) { }

        public Crawler(ILoggerFactory log, CrawlerSettings settings)
        {
            _logger = log.CreateLogger("Crawler") ?? NullLoggerFactory.Instance.CreateLogger("NullCrawler");
            Settings = settings ??= DefaultSettings();
            _logger.LogDebug("Created crawler object");
        }

        private CrawlerSettings DefaultSettings()
        {
            return new CrawlerSettings(RunMode.AllLists, TimeSpan.Zero);
        }

        #endregion

        #region API

        /// <summary>
        /// Adds website to the list of targets for data scraping. 
        /// <para>[Optional] You can specify alias name (key) for this website. If no name (key) is specified then URL is used as the key.</para> 
        /// </summary>
        /// <param name="url"></param>
        public void AddTarget(string name, string url)
        {
            name ??= url;
            TargetSite target = new TargetSite(url);
            _targets.Add(name, target);

            _logger.LogTrace($"Target added. Name: {name}  Url: {url}");
        }

        /// <summary>
        /// Adds website to the list of targets for data scraping. 
        /// <para>[Optional] You can specify alias name (key) for this website. If no name (key) is specified then URL is used as the key.</para> 
        /// </summary>
        /// <param name="name">Name of the website that you can use to refer to the url later on</param>
        /// <param name="target">Object representing target website and it's scraping configuration</param>
        public void AddTarget(string name, TargetSite target)
        {
            if (name == null) throw new ArgumentNullException(nameof(name), "Name cannot be null");
            _targets.Add(name, target);
            _logger.LogTrace($"Target added. Name: {name}  Url: {target.Url}");
        }

        /// <summary>
        /// Adds website to the list of targets for data scraping. 
        /// <para>[Optional] You can specify alias name (key) for this website. If no name (key) is specified then URL is used as the key.</para> 
        /// </summary>
        /// <param name="name">Name of the website that you can use to refer to the url later on</param>
        /// <param name="url">URL to the target website</param>
        /// <param name="path">XPath to the element containing data to scrape</param>
        public void AddTarget(string name, string url, string path)
        {
            name ??= url;
            if (url == null) throw new ArgumentNullException(nameof(url), "Url cannot be null");
            if (path == null) throw new ArgumentNullException(nameof(path), "Path cannot be null");

            TargetSite target = new TargetSite(url, path);
            AddTarget(name, target);

        }

        public void AddTarget(string name, string url, IEnumerable<string> xpathNode)
        {

        }

        public void AddPath(string name, string xpath)
        {

        }

        /// <summary>
        /// Adds webstie to the target list 
        /// </summary>
        /// <param name="url">URL address of the website to scrape the data from</param>
        /// <param name="dataNodes">XPath to the node containing interesting data</param>
        public void AddPath(string name, IEnumerable<string> xpathNode)
        {
            TargetSite target = new TargetSite(name);
        }

        public IEnumerable<string> GetTargetNames()
        {
            return _targets.Keys;
        }

        #endregion
    }
}
