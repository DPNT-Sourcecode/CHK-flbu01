using BeFaster.Runner.Exceptions;
using System.CodeDom;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    //Our price table and offers: 
    //+------+-------+----------------+
    //| Item | Price | Special offers |
    //+------+-------+----------------+
    //| A    | 50    | 3A for 130     | 101 - 132
    //| B    | 30    | 2B for 45      |
    //| C    | 20    |                |
    //| D    | 15    |                |
    //+------+-------+----------------+
    public static class CheckoutSolution
    {
        private static Dictionary<char, int> prices = new Dictionary<char, int>
        {
            { 'A', 50 },
            { 'B', 30 },
            { 'C', 20 },
            { 'D', 15 }
        };
        private static Dictionary<char, (int quantity, int price)> specialOffers = new Dictionary<char, (int quantity, int price)>
        {
            { 'A', (3, 130) },
            { 'B', (2, 45) }
        };
        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus))
            {
                return -1;
            }
            var totalPrice = 0;

            foreach(var c in skus)
            {
                if (!IsCapitalLetter(c))
                {
                    return -1;
                }
            }

            return totalPrice;
        }

        private static bool IsCapitalLetter(char c)
        {
            return c >= 101 && c <= 132;
        }
    }
}



