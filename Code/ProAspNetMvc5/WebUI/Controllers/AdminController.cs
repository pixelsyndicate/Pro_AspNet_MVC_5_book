﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    // Authorize filter enforces access to the methods of the controller work for all authenticated users.
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repo;

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

        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    // there's somethign wrong?
        //    if (!ModelState.IsValid) return View(product);

        //    // nope. move along
        //    _repo.SaveProduct(product);
        //    TempData["message"] = string.Format("{0} has been saved", product.Name);
        //    return RedirectToAction("Index");

        //}

        /// <summary>
        /// this overloaded Edit is used in conjuction with a posted image binary 
        /// </summary>
        /// <param name="product"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    // assign the mimetype
                    product.ImageMimeType = image.ContentType;
                    // make space for the image data
                    product.ImageData = new byte[image.ContentLength];
                    // read the input stream from start to finish
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _repo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            // use the edit form, as it does the same as a new form. 
            // but pass along an empty model so it's got blank entries.
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _repo.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}