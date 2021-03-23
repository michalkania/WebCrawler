using System;

namespace WebCrawler.Models
{
    public interface IHttpCrawlerClient
    {
        string GetHtmlAsync();
    }
}
