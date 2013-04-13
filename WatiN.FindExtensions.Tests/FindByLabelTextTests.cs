using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace WatiN.FindExtensions.Tests
{
    [TestClass]
    public class FindByLabelTextTests
    {
        private readonly string url = Properties.Settings.Default.Url;

        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(LabelText = "First Name:")]
            public TextField FirstNameByLabelText { get; set; }

            [ExtendedFindBy(LabelText = "Frst Name:")]
            public TextField FirstNameNotFoundByLabelText { get; set; }

            [ExtendedFindBy(LabelTextRegex = "^First")]
            public TextField FirstNameByLabelTextRegex { get; set; }

            // missing : at the end of the label regex
            [ExtendedFindBy(LabelTextRegex = "^First Name$")]
            public TextField FirstNameNotFoundByLabelTextRegex { get; set; }
        }

        [TestMethod]
        public void Can_find_first_name_by_label_text()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByLabelText.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_label_text_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByLabelText.Exists);
            }
        }

        [TestMethod]
        public void Can_find_first_name_by_label_text_regex()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByLabelTextRegex.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_label_text_regex_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByLabelTextRegex.Exists);
            }
        }
    }
}
