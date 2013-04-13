using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WatiN.Core;
using System.Collections.Generic;

namespace WatiN.FindExtensions.Tests
{
    [TestClass]
    public class FindByGenericAttributeTests
    {
        private readonly string url = Properties.Settings.Default.Url;
        public class HomeIndexPage : Page
        {
            [ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValue = "data-firstName")]
            public TextField FirstNameByGenericAttributeValue { get; set; }

            [ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValue = "data-firstNam")]
            public TextField FirstNameNotFoundByGenericAttributeValue { get; set; }

            [ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValueRegex = "^data-first")]
            public TextField FirstNameByGenericAttributeValueRegex { get; set; }

            [ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValueRegex = "^data-firstNam$")]
            public TextField FirstNameNotFoundByGenericAttributeValueRegex { get; set; }
        }

        [TestMethod]
        public void Can_find_first_name_by_generic_attribute_text()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByGenericAttributeValue.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_generic_attribute_text_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByGenericAttributeValue.Exists);
            }
        }

        [TestMethod]
        public void Can_find_first_name_by_generic_attribute_text_regex()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsTrue(page.FirstNameByGenericAttributeValueRegex.Exists);
            }
        }

        [TestMethod]
        public void Cannot_find_first_name_by_generic_attribute_text_regex_when_there_is_no_match()
        {
            using (var browser = new IE(url))
            {
                var page = browser.Page<HomeIndexPage>();
                Assert.IsFalse(page.FirstNameNotFoundByGenericAttributeValueRegex.Exists);
            }
        }
    }
}
