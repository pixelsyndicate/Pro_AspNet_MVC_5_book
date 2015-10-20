using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Linq;


using Moq;
using SportsStore.Domain.Abstract;

using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {

            // arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Product, p1);
            Assert.AreEqual(results[1].Product, p2);
            // Assert.Fail("Not Implemented");
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            // arrange - create a new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 11);
            Assert.AreEqual(results[1].Quantity, 1);
            // Assert.Fail("Not Implemented");
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            // arrange - create a new cart
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            // Act
            target.RemoveLine(p2);

            // Assert
            Assert.AreEqual(target.Lines.Where(c => c.Product == p2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
            // Assert.Fail("Not Implemented");
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // arrange - create a new cart
            Cart target = new Cart();

            // act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();

            // Assert
            Assert.AreEqual(result, 450M);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // arrange - create test products
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            // arrange - create a new cart
            Cart target = new Cart();
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // act
            target.Clear();

            // Assert
            Assert.AreEqual(target.Lines.Count(), 0);
        }


        [TestMethod]
        public void Can_Add_To_Cart()
        {
            // arrange - create mock repository (not create collection classes)
            // Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new[] { new Product { ProductID = 1, Name = "P1", Category = "Apples" } }.AsQueryable());
            mock.Setup(m => m.GetProduct(1)).Returns(new Product { ProductID = 1, Name = "P1", Category = "Apples" });

            // arrange - create a new cart
            Cart cart = new Cart();




            // arrange  - create the controller
            CartController target = new CartController(mock.Object, null);

            // act - add a product to the cart
            target.AddToCart(cart, 1, null);

            // Assert
            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void Adding_Product_To_Cart_Goes_To_Cart_Screen()
        {
            // arrange - create mock repository (not create collection classes)
            // Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] { new Product { ProductID = 1, Name = "P1", Category = "Apples" }, }.AsQueryable());

            // arrange - create a new cart
            Cart cart = new Cart();

            // arrange  - create the controller
            CartController target = new CartController(mock.Object, null);

            // act - add a product to the cart
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            // assert
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");
        }

        [TestMethod]
        public void Can_View_Cart_Contents()
        {
            // arrange - creat a cart
            Cart cart = new Cart();

            // arrange create the controller
            CartController target = new CartController(null, new EmailOrderProcessor(new EmailSettings()));

            // act - call the index action method
            CartIndexViewModel result = (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;

            // assert
            Assert.AreSame(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");
        }

        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            // arrange -create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // arrange - create an empty cart
            Cart cart = new Cart();

            // arrange - create shipping details
            ShippingDetails shippingDetails = new ShippingDetails();

            // arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);

            // act - get a result back from the CartController.CheckOut() method
            ViewResult result = target.CheckOut(cart, shippingDetails);

            // assert - verify that processorder method is never called
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            // assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);

            // assert - check that i am passing an invalid model to the view
            Assert.AreEqual(false,result.ViewData.ModelState.IsValid);

        }


        [TestMethod]
        public void Cannot_Checkout_Invalid_Shipping_Data()
        {
            // arrange -create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            // arrange - add a error to the model
            target.ModelState.AddModelError("error","error");

            // act - try to check out
            ViewResult result = target.CheckOut(cart, new ShippingDetails());

            // assert - verify that processorder method is never called
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never);

            // assert - check that the method is returning the default view
            Assert.AreEqual("", result.ViewName);

            // assert - check that i am passing an invalid model to the view
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);

        }


        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // arrange -create a mock order processor
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();

            // arrange - create a cart with an item
            Cart cart = new Cart();
            cart.AddItem(new Product(), 1);

            // arrange - create an instance of the controller
            CartController target = new CartController(null, mock.Object);
            
            // act - try to check out
            ViewResult result = target.CheckOut(cart, new ShippingDetails());

            // assert - check that the order has been passed on to the processor
            mock.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once);

            // assert - check that the method is returning the completed view
            Assert.AreEqual("Completed", result.ViewName);

            // assert - check that i am passing an invalid model to the view
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);

        }

    }
}
