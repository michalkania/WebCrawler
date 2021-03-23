using System;
using WebCrawler.Models;

namespace WebCrawler
{
    /// <summary>
    /// This is main Crawler class. You can add URLs here and configure the way scraping should be performed.
    /// </summary>
    /// <remarks>
    /// Downloads and strips data from the list of targets
    /// </remarks>
    public class Crawler
    {
        public CrawlerSettings Settings { get; private set; }

        TargetManager _targetManager;

        IHttpCrawlerClient _client;

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

        #region API

        #endregion
    }
}
