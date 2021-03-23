using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebCrawler.Models;

namespace Testing
{
    [TestClass]
    public class TargetTest
    {
        #region Comparison

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullUrl_Throws()
        {
            string someUrl = null;
            Target targetSite = new Target(someUrl);
        }

        [TestMethod]
        public void CheckIfEqual_True()
        {
            Target target1 = new Target("https://google.com");
            Target target2 = new Target("https://google.com");
            bool expected = true;

            bool actual = target1 == target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfEqual_False()
        {
            Target target1 = new Target("https://google.com");
            Target target2 = new Target("https://yahoo.com");
            bool expected = false;

            bool actual = target1 == target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfDifferent_True()
        {
            Target target1 = new Target("https://google.com");
            Target target2 = new Target("https://yahoo.com");
            bool expected = true;

            bool actual = target1 != target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfDifferent_False()
        {
            Target target1 = new Target("https://google.com");
            Target target2 = new Target("https://google.com");
            bool expected = false;

            bool actual = target1 != target2;

            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Creation

        [TestMethod]
        [DataRow("https://google.com")]
        [DataRow("https://dev.zilliqa.com")]
        [DataRow("https://reddit.com")]
        [DataRow("https://www.google.com")]
        [DataRow("http://www.google.com")]
        [DataRow("http://www.reddit.com")]
        [DataRow("http://www.yahoo.com")]
        public void Create_WithUrl_Success(string url)
        {
            Target target = new Target(url);
        }

        [TestMethod]
        [DataRow("www.yahoo.com")]
        [DataRow("yahoo.com")]
        [DataRow("httpx://google.com")]
        [DataRow("://google.com")]
        [DataRow("www.google.com")]
        [DataRow("\\google.com")]
        [DataRow("google.com")]
        [DataRow("/google.com")]
        [DataRow("file://google.com")]
        [DataRow("//google.com")]
        [DataRow("mailto://google.com")]
        [DataRow("ftp://google.com")]
        [DataRow("data://google.com")]
        [DataRow("irc://google.com")]
        [ExpectedException(typeof(ArgumentException))]
        public void Create_WithWrongFormatUrl_Throws(string url)
        {
            Target target = new Target(url);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullUrl_Throws()
        {
            string url = null;
            Target target = new Target(url);
        }

        [TestMethod]
        [DataRow("https://google.com", "//body")]
        [DataRow("https://google.com", "/body")]
        [DataRow("https://google.com", "body")]
        [DataRow("https://google.com", ".body")]
        [DataRow("https://google.com", "./body")]
        [DataRow("https://google.com", ".//body")]
        [DataRow("https://google.com", ".././body")]
        [DataRow("https://google.com", "../.././body")]
        [DataRow("https://google.com", "<test>")]
        [DataRow("https://google.com", "^wrong")]
        public void Create_WithCorrectUrlAndPath_Success(string url, string xpath)
        {
            // TODO: Write tests and validate if XPATH is actually a correct one
            Assert.Fail();
            //Target target = new Target(url, xpath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullUrlAndCorrectPath_Throws()
        {
            string url = null;
            string xpath = "//body";
            Target target = new Target(url, xpath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithCorrectUrlandNullPath_Throws()
        {
            string url = "https://google.com";
            string xpath = null;
            Target target = new Target(url, xpath);
        }

        #endregion

        #region AddPath Method Test

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddPath_Null_Throws()
        {
            string xpath = null;
            Target target = new Target("https://google.com");
            target.AddPath(xpath);
        }

        #endregion
    }
}
