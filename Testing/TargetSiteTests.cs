using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebCrawle.Models;

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
            TargetSite targetSite = new TargetSite(someUrl);
        }

        [TestMethod]
        public void CheckIfEqual_True()
        {
            TargetSite target1 = new TargetSite("https://google.com");
            TargetSite target2 = new TargetSite("https://google.com");
            bool expected = true;

            bool actual = target1 == target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfEqual_False()
        {
            TargetSite target1 = new TargetSite("https://google.com");
            TargetSite target2 = new TargetSite("https://yahoo.com");
            bool expected = false;

            bool actual = target1 == target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfDifferent_True()
        {
            TargetSite target1 = new TargetSite("https://google.com");
            TargetSite target2 = new TargetSite("https://yahoo.com");
            bool expected = true;

            bool actual = target1 != target2;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CheckIfDifferent_False()
        {
            TargetSite target1 = new TargetSite("https://google.com");
            TargetSite target2 = new TargetSite("https://google.com");
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
            TargetSite target = new TargetSite(url);
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
            TargetSite target = new TargetSite(url);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullUrl_Throws()
        {
            string url = null;
            TargetSite target = new TargetSite(url);
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
            TargetSite target = new TargetSite(url, xpath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithNullUrlAndCorrectPath_Throws()
        {
            string url = null;
            string xpath = "//body";
            TargetSite target = new TargetSite(url, xpath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_WithCorrectUrlandNullPath_Throws()
        {
            string url = "https://google.com";
            string xpath = null;
            TargetSite target = new TargetSite(url, xpath);
        }

        #endregion

        #region AddPath Method Test

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddPath_Null_Throws()
        {
            string xpath = null;
            TargetSite target = new TargetSite("https://google.com");
            target.AddPath(xpath);
        }

        #endregion
    }
}
