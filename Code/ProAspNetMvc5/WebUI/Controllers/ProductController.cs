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

        public ViewResult List(string category, int page = 1)
        {
            //IEnumerable<Product> products = _repo.Products
            //    .OrderBy(p => p.ProductID)
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize);

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = _repo.Products
                .Where(p => category == null || p.Category == category) // category filter only used if parm category==null
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo { CurrentPage = page, 
                    ItemsPerPage = PageSize, 
                    TotalItems = // - adjusting it to account for category filters _repo.Products.Count() 
                    category == null ? _repo.Products.Count() : _repo.Products.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }

        //public ViewResult List()
        //{
        //    return View(_repo.Products);
        //}

        public FileContentResult GetImage(int productid)
        {
            Product prod = _repo.Products.FirstOrDefault(p => p.ProductID == productid);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
            throw new NotImplementedException();
        }
    }
}