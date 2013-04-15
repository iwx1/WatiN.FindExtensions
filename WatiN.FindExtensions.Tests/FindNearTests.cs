using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatiN.Core;

namespace WatiN.FindExtensions.Tests
{
    [TestClass]
    public class FindNearTests
    {
        private readonly string url = Properties.Settings.Default.Url;

        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(NearText = "Last Name:")]
            public TextField LastNameTextBox { get; set; }

            [ExtendedFindBy(NearText = "Index")]
            public TextField LastNameTextBoxNotFound { get; set; }
        }

        [TestMethod]
        public void Can_find_last_name_textbox_near_label_text()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.LastNameTextBox.Exists);
            }
        }

        [TestMethod]
        public void Can_find_first_name_textbox_near_index_header()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.LastNameTextBoxNotFound.Exists);
                Assert.AreEqual("firstName", page.LastNameTextBoxNotFound.IdOrName);
            }
        }
    }
}
