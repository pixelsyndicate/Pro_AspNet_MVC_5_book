using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EssentialTools.Models
{
    public class DefaultDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalParam)
        {
            // this demo would return 10% of the total? 19.999 on a total of 199.99
            return (totalParam - (10m / 100m * totalParam));
        }
    }


}