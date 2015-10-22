using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void Can_Do_Stuff()
        {
            // Arrange - Set up what i`m going to work with
            IEnumerable<int> numbers = new[] {0, 1, 2, 3, 4, 5};

            // Act - Do the Stuff
            var avg = numbers.Sum()/numbers.Count(n => n != 0);

            // Assert - Validate it worked
            Assert.AreEqual(avg, 3);
            Assert.AreEqual(numbers.Sum(), 15);
            Assert.IsTrue(numbers.Count() == 6);
            Assert.IsTrue(numbers.Count(n => n != 0) == 5);

        }
    }
}
