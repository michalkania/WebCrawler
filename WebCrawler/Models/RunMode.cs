using System.ComponentModel;

namespace WebCrawler.Models
{
    /// <summary>
    /// How crawler will handle current execution
    /// </summary>
    public enum RunMode
    {
        [Description("Will go through every saved or passed list of pages.")]
        OneList,
        [Description("Will go through the chosen lists.")]
        MultiList,
        [Description("Will go through every saved and passed list.")]
        AllLists
    }

    public enum CrawlerSaveMode
    {
        None,
        OneFile,
        MultiFile,
        Verbose
    }
}
