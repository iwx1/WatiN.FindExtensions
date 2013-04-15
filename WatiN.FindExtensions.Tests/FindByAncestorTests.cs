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
    public class FindByAncestorTests
    {
        private readonly string url = Properties.Settings.Default.Url;

        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(AncestorAttributeName = "id", AncestorAttributeValue = "relatedElementParent")]
            public TextField UnidentifiedChildTextField { get; set; }

            [ExtendedFindBy(AncestorAttributeName = "name", AncestorAttributeValue = "relatedElementParent")]
            public TextField UnidentifiedChildTextFieldNotFound { get; set; }

            [ExtendedFindBy(AncestorAttributeName = "id", AncestorAttributeValueRegex = "^related")]
            public TextField UnidentifiedChildTextFieldByRegex { get; set; }

            [ExtendedFindBy(AncestorAttributeName = "id", AncestorAttributeValueRegex = "^related$")]
            public TextField UnidentifiedChildTextFieldByRegexNotFound { get; set; }
        }

        [TestMethod]
        public void Can_find_unidentified_child_text_field_by_ancestor()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.UnidentifiedChildTextField.Exists);
                Assert.AreEqual("child", page.UnidentifiedChildTextField.Value);
            }
        }

        [TestMethod]
        public void Cannot_find_unidentified_child_text_field_by_ancestor()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.UnidentifiedChildTextFieldNotFound.Exists);
            }
        }

        [TestMethod]
        public void Can_find_unidentified_child_text_field_by_ancestor_regex()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.UnidentifiedChildTextFieldByRegex.Exists);
                Assert.AreEqual("child", page.UnidentifiedChildTextFieldByRegex.Value);
            }
        }

        [TestMethod]
        public void Cannot_find_unidentified_child_text_field_by_ancestor_regex()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.UnidentifiedChildTextFieldByRegexNotFound.Exists);
            }
        }
    }
}
