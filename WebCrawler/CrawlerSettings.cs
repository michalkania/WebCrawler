using System;
using WebCrawle.Models;

namespace WebCrawler
{
    public class CrawlerSettings
    {
        /// <summary>
        /// How data and execution is handled. Can be overriden by Crawler depending on the passed settings.
        /// </summary>
        public RunMode RunMode { get; set; }


        /// <summary>
        /// [Optional] Time between crawler executions. 
        /// <para>Default is 0 once</para>
        /// </summary>
        public TimeSpan ExecPeriod { get; set; }

        public CrawlerSettings()
        {
            RunMode = RunMode.AllLists;
            ExecPeriod = TimeSpan.Zero;
        }

        public CrawlerSettings(RunMode mode, TimeSpan execTime)
        {
            RunMode = mode;
            ExecPeriod = execTime;
        }
    }
}
