using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace WatiN.FindExtensions.Tests
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Confirm_site_is_running()
        {
            using (var browser = new IE("http://localhost:31337/"))
            {
                Assert.IsTrue(browser.ContainsText("Index"));
            }
        }
    }
}
