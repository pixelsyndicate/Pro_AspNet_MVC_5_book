using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private IProductRepository _repo;

        public AdminController(IProductRepository repo)
        {
            _repo = repo;
        }

        // GET: Admin
        public ViewResult Index()
        {
            return View(_repo.Products);
        }


        public ViewResult Edit(int productid)
        {
            // get the latest from the db
            Product product = _repo.Products.FirstOrDefault(p => p.ProductID == productid);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there's somethign wrong
                return View(product);
            }
        }
    }
}