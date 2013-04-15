using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace WatiN.FindExtensions.Tests
{
    /// <summary>
    /// Summary description for FindByRelTests
    /// </summary>
    [TestClass]
    public class FindByRelTests
    {
        private readonly string url = Properties.Settings.Default.Url;

        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(RelText = "first-name")]
            public TextField FirstNameByLabelText { get; set; }

            [ExtendedFindBy(RelText = "first name")]
            public TextField FirstNameNotFoundByLabelText { get; set; }

            [ExtendedFindBy(RelTextRegex = "^first-")]
            public TextField FirstNameByLabelTextRegex { get; set; }

            [ExtendedFindBy(RelTextRegex = "^first name$")]
            public TextField FirstNameNotFoundByLabelTextRegex { get; set; }
        }

        [TestMethod]
        public void Can_find_first_name_by_rel_text()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByLabelText.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_rel_text_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByLabelText.Exists);
            }
        }

        [TestMethod]
        public void Can_find_first_name_by_rel_text_regex()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByLabelTextRegex.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_rel_text_regex_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByLabelTextRegex.Exists);
            }
        }
    }
}
