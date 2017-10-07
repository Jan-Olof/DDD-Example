using Helpers.Functional;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HelpersTests
{
    [TestClass]
    public class ListFormatterTests
    {
        [TestMethod]
        public void TestShouldFormatToNumberedListInParallel()
        {
            // Given
            var list = new List<string> { "the first", "THE SECOND", "tHe Third" };

            // When
            var result = ListFormatter.FormatParallel(list);

            // Then
            Assert.AreEqual("1. The first", result[0]);
            Assert.AreEqual("2. The second", result[1]);
            Assert.AreEqual("3. The third", result[2]);
        }

        [TestMethod]
        public void TestShouldFormatToNumberedListSequentially()
        {
            // Given
            var list = new List<string> { "the first", "THE SECOND", "tHe Third" };

            // When
            var result = ListFormatter.FormatSequential(list);

            // Then
            Assert.AreEqual("1. The first", result[0]);
            Assert.AreEqual("2. The second", result[1]);
            Assert.AreEqual("3. The third", result[2]);
        }
    }
}