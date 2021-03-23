using System.Collections.Generic;
using WebCrawler.Models;

namespace WebCrawler.Models
{
    public class CrawlerList
    {
        public string Name { get; set; }

        /// <summary>
        /// List of websites to be crawled
        /// </summary>
        public IList<Target> TargetSites { get; set; }

    }
}
