using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebCrawle.Models;
using WebCrawler;

namespace Testing
{
    [TestClass]
    public class CrawlerApi
    {
        [TestMethod]
        public void CheckDefaultSetting_RunMode_IsAllLists()
        {
            Crawler crawler = new Crawler();

            var expected = RunMode.AllLists;
            RunMode actual = crawler.Settings.RunMode;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckDefaultSetting_ExecPeriod_IsZero()
        {

            Crawler crawler = new Crawler();

            var expected = TimeSpan.Zero;
            TimeSpan actual = crawler.Settings.ExecPeriod;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddTarget_IsOnList()
        {

        }

    }
}
