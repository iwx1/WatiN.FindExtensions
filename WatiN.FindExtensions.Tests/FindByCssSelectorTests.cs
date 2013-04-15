using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;

namespace WatiN.FindExtensions.Tests
{
    [TestClass]
    public class FindByCssSelectorTests
    {
        private readonly string url = Properties.Settings.Default.Url;

        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(CssSelectorText = "#firstName")]
            public TextField FirstNameBySelector { get; set; }

            [ExtendedFindBy(CssSelectorText = ".firstName")]
            public TextField FirstNameByIncorrectSelector { get; set; }

            [ExtendedFindBy(CssSelectorText = "div > div.row > label")]
            public Label FirstNameLabelBySelector { get; set; }
        }

        [TestMethod]
        public void Can_find_firstName_by_id_selector()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameBySelector.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_firstName_by_id_selector_when_wrong()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameByIncorrectSelector.Exists);
            }
        }

        [TestMethod]
        public void Can_find_firstName_by_children_selector()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameLabelBySelector.Exists);
                Assert.AreEqual("First Name:", page.FirstNameLabelBySelector.Text);
            }
        }
    }
}
