using BeFaster.Runner.Exceptions;
using System.CodeDom;
using System.Collections.Generic;

namespace BeFaster.App.Solutions.CHK
{
    // Round 1
    //+------+-------+----------------+
    //| Item | Price | Special offers |
    //+------+-------+----------------+
    //| A    | 50    | 3A for 130     |
    //| B    | 30    | 2B for 45      |
    //| C    | 20    |                |
    //| D    | 15    |                |
    //+------+-------+----------------+
    // Round 2
    //+------+-------+------------------------+
    //| Item | Price | Special offers         |
    //+------+-------+------------------------+
    //| A    | 50    | 3A for 130, 5A for 200 |
    //| B    | 30    | 2B for 45              |
    //| C    | 20    |                        |
    //| D    | 15    |                        |
    //| E    | 40    | 2E get one B free      |
    //+------+-------+------------------------+
    public class SpecialOffer 
    {
        public SpecialOffer(int quantity, int? price, char? itemOffer = null)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.ItemOffer = itemOffer;
        }
        public int Quantity { get; }
        public int? Price { get; }
        public char? ItemOffer { get; }
    }

    public static class CheckoutSolution
    {
        private static Dictionary<char, int> prices = new Dictionary<char, int>
        {
            { 'A', 50 },
            { 'B', 30 },
            { 'C', 20 },
            { 'D', 15 },
            { 'E', 40 }
        };
        private static Dictionary<char, SpecialOffer> specialOffers = new Dictionary<char, SpecialOffer>
        {
            { 'A', new SpecialOffer(3, 130) },
            { 'B', new SpecialOffer(2, 45) },
            { 'E', new SpecialOffer(2, null, 'B') }
        };
        public static int ComputePrice(string skus)
        {
            if (string.IsNullOrEmpty(skus))
            {
                return 0;
            }
            var itemQuantities = new Dictionary<char, int>();

            foreach(var c in skus)
            {
                if (!IsCapitalLetter(c))
                {
                    return -1;
                }

                if (itemQuantities.TryGetValue(c, out var quantity))
                {
                    itemQuantities[c] = quantity + 1;
                }
                else
                {
                    itemQuantities.Add(c, 1);
                }
            }

            var totalPrice = 0;

            foreach (var item in itemQuantities)
            {
                if(!prices.TryGetValue(item.Key, out var price))
                {
                    return -1;
                }

                if (specialOffers.TryGetValue(item.Key, out var specialOffer) && specialOffer.Quantity <= item.Value)
                {
                    var offerMultiplier = item.Value / specialOffer.Quantity;
                    var remainder = item.Value - (offerMultiplier * specialOffer.Quantity);
                    totalPrice += (offerMultiplier * specialOffer.Price) + (remainder * price);
                }
                else
                {
                    totalPrice += price * item.Value;
                }
            }

            return totalPrice;
        }

        private static bool IsCapitalLetter(char c)
        {
            return c >= 65 && c <= 90;
        }
    }
}



