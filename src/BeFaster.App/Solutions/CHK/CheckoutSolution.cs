using BeFaster.Runner.Exceptions;
using System.CodeDom;
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
        private static Dictionary<char, (int Quantity, int Price)> specialOffers = new Dictionary<char, (int Quantity, int Price)>
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
            var checkoutItemQuantities = new Dictionary<char, int Quantity>();

            foreach(var c in skus)
            {
                if (!IsCapitalLetter(c))
                {
                    return -1;
                }

                if (checkoutItemQuantities.TryGetValue(c, out var quantity))
                {
                    checkoutItemQuantities[c] = quantity + 1;
                }
                else
                {
                    checkoutItemQuantities.Add(c, 1);
                }
            }

            var totalPrice = 0;

            foreach (var checkoutItem in checkoutItemQuantities)
            {
                if (specialOffers.TryGetValue(checkoutItem.Key, out var specialOffer) && specialOffer.Quantity <= checkoutItem.Value)
                {
                    var offerMultiplier = checkoutItem.Value / specialOffer.Quantity; 
                    totalPrice += offerMultiplier * specialOffer.Price + ;
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





