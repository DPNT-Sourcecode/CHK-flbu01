using System;
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
        public SpecialOffer(int quantity, int? price, char? item = null)
        {
            this.Quantity = quantity;
            this.Price = price;
            this.Item = item;
        }
        public int Quantity { get; }
        public int? Price { get; }
        public char? Item { get; }
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
            var skuQuantities = new Dictionary<char, int>();

            foreach (var c in skus)
            {
                if (!IsCapitalLetter(c))
                {
                    return -1;
                }

                if (skuQuantities.TryGetValue(c, out var quantity))
                {
                    skuQuantities[c] = quantity + 1;
                }
                else
                {
                    skuQuantities.Add(c, 1);
                }
            }

            var totalPrice = 0;

            foreach (var sku in skuQuantities)
            {
                if (!prices.TryGetValue(sku.Key, out var item))
                {
                    return -1;
                }

                totalPrice += CalculateItemPrice(sku.Value, item);

                totalPrice -= CalculateDiscount(skuQuantities, sku.Value, item);
            }

            return totalPrice;
        }

        private static bool IsCapitalLetter(char c)
        {
            return c >= 65 && c <= 90;
        }

        private static int CalculateDiscount(Dictionary<char, int> skuQuantities, int quantity, Item item)
        {
            if (item.SpecialOffer?.Item == null
                || !prices.TryGetValue(item.SpecialOffer.Item.Value, out var offeredItem)
                || !skuQuantities.TryGetValue(item.SpecialOffer.Item.Value, out var offeredItemQuantity))
            {
                return 0;
            }

            var newOfferedItemQuantity = Math.Max(offeredItemQuantity - (quantity / item.SpecialOffer.Quantity), 0);

            return Math.Min(CalculateItemPrice(offeredItemQuantity, offeredItem), CalculateItemPrice(newOfferedItemQuantity, offeredItem));
        }

        private static int CalculateItemPrice(int quantity, Item item)
        {
            if (item.SpecialOffer?.Price != null && quantity >= item.SpecialOffer.Quantity)
            {
                var offerMultiplier = quantity / item.SpecialOffer.Quantity;
                var remainder = quantity - (offerMultiplier * item.SpecialOffer.Quantity);
                return (offerMultiplier * item.SpecialOffer.Price.Value) + (remainder * item.Price);
            }

            return quantity * item.Price;

        }
    }
}


