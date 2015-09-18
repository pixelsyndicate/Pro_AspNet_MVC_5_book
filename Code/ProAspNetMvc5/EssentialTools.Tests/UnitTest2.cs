using System;
using System.Linq;
using EssentialTools.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest2
    {

        private readonly Product[] _products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer Ball", Category = "Soccer", Price = 19.50M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };

        [TestMethod]
        public void Sum_Products_Correctly()
        {

            // arrange

            // the reason we'd use MOQ's here is because our LinkValueCalculator has a dependency
            // on the IDiscountHelper implementation. so if we have a test fail,
            // we don't know if it's the Implimentation of the discounter or if
            // it's the linqvaluecalculator


            // IDiscountHelper discounter = new MinimumDiscountHelper();
            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            // we inform the mock that i will be passing in a decimal and getting back a decimal
            mock.Setup(m =>
                m.ApplyDiscount(It.IsAny<decimal>()))
                .Returns<decimal>(total => total);
            // this doesn't implement the mocked classes internal logic however.

            LinqValueCalculator target = new LinqValueCalculator(mock.Object);

            // act
            // this returns _discounter.ApplyDiscount(products.Sum(p => p.Price));
            var result = target.ValueProducts(_products);

            // assert
            Assert.AreEqual(_products.Sum(e => e.Price), result);
        }


        // now to moq the behavior of the minimimdiscounthelper class

        private Product[] createProduct(decimal value)
        {
            return new[] { new Product { Price = value } };
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void Pass_Through_Variable_Discounts()
        {
            // arrange
            // mock-up the entire logic in the implementation?


            Mock<IDiscountHelper> mock = new Mock<IDiscountHelper>();
            // TODO: remember that .Setup() runs in reverse order than the coded order! :/


            // "no discount is applied on totals lt $10" (this is the 'else')
            mock.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            // "ArgumentoutOfRangeException will be thrown on negatives"
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v == 0))).Throws<ArgumentOutOfRangeException>();

            // "if the total gt $100, discount will be 10%"
            mock.Setup(m => m.ApplyDiscount(It.Is<decimal>(v => v > 100))).Returns<decimal>(total => (total * 0.9M));

            // "if total gt $10 && lt $100 inclusive, discount will be $5"
            mock.Setup(m => m.ApplyDiscount(It.IsInRange<decimal>(10, 100, Range.Inclusive))).Returns<decimal>(total => total - 5);

            var target = new LinqValueCalculator(mock.Object);


            // act 
            decimal fiveDollarAmount = target.ValueProducts(createProduct(5));
            decimal tenDollarAmount = target.ValueProducts(createProduct(10));
            decimal fiftyDollarAmount = target.ValueProducts(createProduct(50));
            decimal oneHunderedDollarAmount = target.ValueProducts(createProduct(100));
            decimal fiveHundredDollarAmount = target.ValueProducts(createProduct(500));

            decimal negativeDollarAmount = target.ValueProducts(createProduct(-1));


            // assert
            Assert.AreEqual(5, fiveDollarAmount, "$5 Fail");
            Assert.AreEqual(5, tenDollarAmount, "$10 Fail");
            Assert.AreEqual(45, fiftyDollarAmount, "$50 Fail");
            Assert.AreEqual(95, oneHunderedDollarAmount, "$100 Fail");
            Assert.AreEqual(450, fiveHundredDollarAmount, "$500 Fail");

            target.ValueProducts(createProduct(0));
        }

    }
}
