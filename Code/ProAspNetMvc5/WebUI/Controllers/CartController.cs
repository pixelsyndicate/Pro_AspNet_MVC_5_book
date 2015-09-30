using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repo;

        public CartController(IProductRepository repo)
        {
            _repo = repo;
        }
        public ActionResult Index(string returnurl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnurl
            });
        }

        public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repo.GetProduct(productId);
            if (product != null)
                GetCart().AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repo.GetProduct(productId);
            if (product != null)
                GetCart().RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        ///     Keeping the Cart in session
        /// </summary>
        /// <returns></returns>
        private Cart GetCart()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null) return cart;
            cart = new Cart();
            Session["cart"] = cart;
            return cart;
        }


    }
}