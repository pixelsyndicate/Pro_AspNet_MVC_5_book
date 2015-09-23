using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            // act
            IEnumerable<Product> result = (IEnumerable<Product>)controller.List(2).Model;

            // assert
            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // arrange  - define an html helper - we need to do this in order to appy the extension method
            HtmlHelper myHelper = null;

            // arrange - create the paginginfo data
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            // arrange - setup the delegate using a lambda expression
            // incoming parm i is int
            // return is string
            Func<int, string> pageUrlDelegate = i => "Page " + i;

            // act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            var shouldBe = @"<a class=""btn btn-default"" href=""Page 1"">1</a><a class=""btn btn-default btn-primary selected"" href=""Page 2"">2</a><a class=""btn btn-default"" href=""Page 3"">3</a>";

            // assert
            Assert.AreEqual(shouldBe, result.ToString());

        }
    }
}
