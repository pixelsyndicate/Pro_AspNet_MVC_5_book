using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    /// <summary>
    ///     I will use Html.Action to inject action returns into the View
    /// </summary>
    public class NavController : Controller
    {
        private readonly IProductRepository _repo;

        public NavController(IProductRepository repo)
        {
            _repo = repo;
        }

        public PartialViewResult Menu(string category = null, bool horizontalLayout = false)
        {
            // rather than hold the list of categories and the currently selected category in my viewmodel, i'm gonna cheat a bit and put it into a viewbag
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = _repo.Products.Select(p => p.Category).Distinct().OrderBy(x => x);

            // added to support different menu for small browsers
            string viewName = horizontalLayout ? "MenuHorizontal" : "Menu";
            return PartialView(viewName, categories);


            // returning a partialview passes the model to a template PartialView.
            // return PartialView(categories);

        }
    }
}