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
    public class Item
    {
        public Item(int price, SpecialOffer specialOffer = null)
        {
            this.Price = price;
            this.SpecialOffer = specialOffer;
        }

        public int Price { get; }

        public SpecialOffer SpecialOffer { get; }
    }
    
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
        private static Dictionary<char, Item> prices = new Dictionary<char, Item>
        {
            { 'A', new Item(50, new SpecialOffer(3, 130)) },
            { 'B', new Item(30, new SpecialOffer(2, 45)) },
            { 'C', new Item(20) },
            { 'D', new Item(15) },
            { 'E', new Item(40, new SpecialOffer(2, null, 'B')) }
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

                if (price.SpecialOffer != null)
                {
                    //var offerMultiplier = item.Value / specialOffer.Quantity;
                    //var remainder = item.Value - (offerMultiplier * specialOffer.Quantity);
                    //totalPrice += (offerMultiplier * specialOffer.Price.Value) + (remainder * price);
                    totalPrice += CalculateItemPrice(item.Value, price);
                }
                else
                {
                    if (itemQuantities.TryGetValue(specialOffer.ItemOffer.Value, out var itemOfferQuantity))
                    {
                        totalPrice -= CalculateDiscount(specialOffer.ItemOffer.Value, itemOfferQuantity);
                    }
                    totalPrice += price * item.Value;
                }
            }

            return totalPrice;
        }

        private static bool IsCapitalLetter(char c)
        {
            return c >= 65 && c <= 90;
        }

        private static int CalculateDiscount(char item, int quantity)
        {
            if (!prices.TryGetValue(item, out var price))
            {
                return 0;
            }
        }

        private static int CalculateItemPrice(int quantity, Item item)
        {
            if(spec)
            var offerMultiplier = quantity / specialOffer.Quantity;
            var remainder = quantity - (offerMultiplier * specialOffer.Quantity);
            return (offerMultiplier * specialOffer.Price.Value) + (remainder * price);

        }
    }
}


