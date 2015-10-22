using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            // arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product {ProductID = 1, Name = "p1"}, new Product {ProductID = 2, Name = "p2"},
                new Product {ProductID = 3, Name = "p3"}
            });

            // arrange - create a controller
            AdminController target = new AdminController(mock.Object);

            // action
            // example of what worked elsewhere - (CartIndexViewModel)target.Index(cart, "myUrl").ViewData.Model;
          
            Product[] results = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual("p1", results[0].Name);
            Assert.AreEqual("p2", results[1].Name);
            Assert.AreEqual("p3", results[2].Name);
        }
    }
}