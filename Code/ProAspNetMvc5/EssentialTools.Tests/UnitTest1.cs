using System;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EssentialTools.Tests
{

    /// <summary>
    /// Demonstrate test driven development (page 137)
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        // we want to TDD this to demonstrate:
        // "if the total gt $100, discount will be 10%"
        // "if total gt $10 && lt $100 inclusive, discount will be $5"
        // "no discount is applied on totals lt $10"
        // "ArgumentoutOfRangeException will be thrown on negatives"
        // there's a ExpectedException attribute useful to ensure exceptions are thrown, so we don't have to use try/catch in our unit tests

        private IDiscountHelper getTestObject() { return new MinimumDiscountHelper(); }

        [TestMethod]
        public void Discount_Above_100()
        {
            // arrange
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            // act
            var discountedTotal = target.ApplyDiscount(total);

            // assert
            // is the discount total = 90% of the total?
            Assert.AreEqual(total * 0.9M, discountedTotal);
        }

        [TestMethod]
        public void Discount_Less_Than_10()
        {
            // arrange
            IDiscountHelper target = getTestObject();

            // act
            var discount5 = target.ApplyDiscount(5);
            var discount0 = target.ApplyDiscount(0);

            // assert
            // "no discount is applied on totals lt $10"
            Assert.AreEqual(5, discount5);
            Assert.AreEqual(0, discount0);
        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            // arrange
            IDiscountHelper target = getTestObject();

            // act
            decimal tenDollarDiscount = target.ApplyDiscount(10);
            decimal hunderedDollarDiscount = target.ApplyDiscount(100);
            decimal fiftyDollarDiscount = target.ApplyDiscount(50);

            // assert
            // "no discount is applied on totals lt $10"
            Assert.AreEqual(5, tenDollarDiscount, "$10 discount is wrong");
            // "if total gt $10 && lt $100 inclusive, discount will be $5"
            Assert.AreEqual(95, hunderedDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 discount is wrong");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            // arrange
            IDiscountHelper target = getTestObject();

            // act
            target.ApplyDiscount(-1);

            // assert
        }
    }
}
