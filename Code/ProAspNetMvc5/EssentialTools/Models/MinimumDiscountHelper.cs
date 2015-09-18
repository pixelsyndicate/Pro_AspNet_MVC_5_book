using System;
using System.Security.Principal;

namespace EssentialTools.Models
{
    /// <summary>
    ///  this is used as a testable class
    /// </summary>

    public class MinimumDiscountHelper : IDiscountHelper
    {
        /// <summary>
        /// we want to TDD this to demonstrate:
        /// "if the total gt $100, discount will be 10%"
        /// "if total gt $10 && lt $100 inclusive, discount will be $5"
        /// "no discount is applied on totals lt $10"
        /// "ArgumentoutOfRangeException will be thrown on negatives"
        /// </summary>
        /// <param name="totalParam"></param>
        /// <returns></returns>
        public decimal ApplyDiscount(decimal totalParam)
        {

            if (totalParam < 0)
            {
                // "ArgumentoutOfRangeException will be thrown on negatives"
                throw new ArgumentOutOfRangeException();
            }
            else if (totalParam > 100)
            {
                // "if the total gt $100, discount will be 10%"
                return totalParam * .9M;
            }
            else if (totalParam >= 10 && totalParam <= 100)
            {
                // "if total gt $10 && lt $100 (inclusive), discount will be $5"
                return totalParam - 5;
            }
            else
            {
                // "no discount is applied on totals lt $10"
                return totalParam;
            }

            throw new NotImplementedException();
        }
    }
}