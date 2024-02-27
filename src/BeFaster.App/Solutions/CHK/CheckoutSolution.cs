using BeFaster.Runner.Exceptions;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    //Our price table and offers: 
    //+------+-------+----------------+
    //| Item | Price | Special offers |
    //+------+-------+----------------+
    //| A    | 50    | 3A for 130     |
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
            return -1;
        }
    }
}


