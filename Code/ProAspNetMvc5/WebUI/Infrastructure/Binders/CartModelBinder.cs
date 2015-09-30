using System.Web.Mvc;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure.Binders
{
    /// <summary>
    ///     So i can unit test values held in session without creating mocks of session, i'm using a custom model binder to
    ///     extract session vals and pass them to action methods in my controllers
    /// </summary>
    public class CartModelBinder : IModelBinder
    {
        private const string SessionKey = "Cart";

        /// <summary>
        /// This returns a CART object from out of session. This way I don't have to pass it in as a parameter in methods. 
        /// I just have to declare it IS a parameter. Kinda like Dependency Injection 
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="bindingContext"></param>
        /// <returns></returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // get the cart from session

            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart) controllerContext.HttpContext.Session[SessionKey];
            }

            // create the cart if there wasn't one in session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[SessionKey] = cart;
                }
            }

            // return the cart
            return cart;
        }
    }
}