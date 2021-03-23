using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
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
        #region Pub Properties

        /// <summary>
        /// Crawler's behavior
        /// </summary>
        public CrawlerSettings Settings { get; private set; }

        #endregion

        #region Priv Properties

        TargetManager targetManager { get; }

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

        #region API

        #endregion
    }
}
