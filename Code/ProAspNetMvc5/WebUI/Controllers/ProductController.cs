using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

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

        public ViewResult List(int page = 1)
        {
            IEnumerable<Product> products = _repo.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);
            return View(products);
        }

        //public ViewResult List()
        //{
        //    return View(_repo.Products);
        //}

    }
}