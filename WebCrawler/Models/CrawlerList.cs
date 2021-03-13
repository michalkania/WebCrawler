using System.Collections.Generic;

namespace WebCrawle.Models
{
    public class CrawlerList
    {
        public string Name { get; set; }

        /// <summary>
        /// List of websites to be crawled
        /// </summary>
        public IList<TargetSite> TargetSites { get; set; }

    }
}
