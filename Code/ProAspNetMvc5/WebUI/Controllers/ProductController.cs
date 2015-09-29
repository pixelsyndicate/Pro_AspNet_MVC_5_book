using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        public int PageSize = 4;

        public ProductController(IProductRepository productRepo)
        {
            _repo = productRepo;
        }

        public ViewResult List(int requestedPage = 1)
        {
            //IEnumerable<Product> products = _repo.Products
            //    .OrderBy(p => p.ProductID)
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize);

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _repo.Products.OrderBy(p => p.ProductID).Skip((requestedPage - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = requestedPage, ItemsPerPage = PageSize, TotalItems = _repo.Products.Count() }
            };

            return View(model);
        }

        //public ViewResult List()
        //{
        //    return View(_repo.Products);
        //}

    }
}