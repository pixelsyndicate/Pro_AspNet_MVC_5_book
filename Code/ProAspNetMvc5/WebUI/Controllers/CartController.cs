using System.Linq;
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



        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel()
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repo.GetProduct(productId);
            //product = _repo.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = _repo.GetProduct(productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        // something to allow the user to see a summary of the current cart contents
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// this older copy uses GetCart() method to handle session. That's now handled by a custom model binder in Infrastructure/binders/
        /// </summary>
        //public RedirectToRouteResult AddToCart(int productId, string returnUrl)
        //{
        //    Product product = _repo.GetProduct(productId);
        //    if (product != null)
        //        GetCart().AddItem(product, 1);
        //    return RedirectToAction("Index", new { returnUrl });
        //}
        /// <summary>
        /// this older copy uses GetCart() method to handle session. That's now handled by a custom model binder in Infrastructure/binders/
        /// </summary>
        //public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
        //{
        //    Product product = _repo.GetProduct(productId);
        //    if (product != null)
        //        GetCart().RemoveLine(product);
        //    return RedirectToAction("Index", new { returnUrl });
        //}
        /// <summary>
        /// this older copy uses GetCart() method to handle session. That's now handled by a custom model binder in Infrastructure/binders/
        /// </summary>
        //public ActionResult Index(string returnurl)
        //{
        //    return View(new CartIndexViewModel
        //    {
        //        Cart = GetCart(),
        //        ReturnUrl = returnurl
        //    });
        //}
        /// <summary>
        ///     Retrieving the Cart in (Cart)Session["cart"] 
        ///     ... this can be make obsolete if I create a custom ModelBinder (CartModelBinder.cs)
        /// </summary>

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["cart"];
            if (cart != null) return cart;
            cart = new Cart();
            Session["cart"] = cart;
            return cart;
        }


        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
    }
}