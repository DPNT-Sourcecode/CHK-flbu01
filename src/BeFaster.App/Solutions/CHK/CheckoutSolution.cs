using System;
using System.Collections.Generic;
using System.Linq;

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
        public Item(int price, List<SpecialOffer> specialOffers = null)
        {
            this.Price = price;
            this.SpecialOffers = specialOffers ?? new List<SpecialOffer>();
        }

        public int Price { get; }

        public List<SpecialOffer> SpecialOffers { get; }

        public SpecialOffer GetBestSpecialOffer(int quantity)
        {
            SpecialOffer value = null;
            foreach (var specialOffer in this.SpecialOffers)
            {
                if (specialOffer.Quantity > quantity)
                {
                    continue;
                }

                if (value == null)
                {
                    value = specialOffer;
                    continue;
                }

                if (specialOffer.CalculateItemPrice(quantity, this.Price) < value.CalculateItemPrice(quantity, this.Price))
                {
                    value = specialOffer;
                }
            }

            return value;
        }

        public SpecialOffer GetSpecialOfferForOtherItems(int quantity)
        {
            return this.SpecialOffers.FirstOrDefault(x => x.Quantity <= quantity && x.Item.HasValue);
        }
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

        public int CalculateItemPrice(int quantity, int unitPrice)
        {
            if (this.Price != null && quantity >= this.Quantity)
            {
                var offerMultiplier = quantity / this.Quantity;
                var remainder = quantity - (offerMultiplier * this.Quantity);
                var totalPrice = (offerMultiplier * this.Price.Value) + (remainder * unitPrice);

                return totalPrice;
            }

            return quantity * unitPrice;
        }
    }

    public static class CheckoutSolution
    {
        private static Dictionary<char, Item> prices = new Dictionary<char, Item>
        {
            { 'A', new Item(50, new List<SpecialOffer>{ new SpecialOffer(3, 130), new SpecialOffer(5, 200) }) },
            { 'B', new Item(30, new List<SpecialOffer>{ new SpecialOffer(2, 45) }) },
            { 'C', new Item(20) },
            { 'D', new Item(15) },
            { 'E', new Item(40, new List<SpecialOffer>{ new SpecialOffer(2, null, 'B') }) }
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
            var specialOffer = item.GetSpecialOfferForOtherItems(quantity);
            if (specialOffer?.Item == null
                || !prices.TryGetValue(specialOffer.Item.Value, out var offeredItem)
                || !skuQuantities.TryGetValue(specialOffer.Item.Value, out var offeredItemQuantity))
            {
                return 0;
            }

            var newOfferedItemQuantity = Math.Max(offeredItemQuantity - (quantity / specialOffer.Quantity), 0);

            var offeredItemTotalPrice = CalculateItemPrice(offeredItemQuantity, offeredItem);
            var newOfferedItemTotalPrice = CalculateItemPrice(newOfferedItemQuantity, offeredItem);

            if (newOfferedItemTotalPrice < offeredItemTotalPrice)
            {
                return offeredItemTotalPrice - newOfferedItemTotalPrice;
            }

            return 0;
        }

        private static int CalculateItemPrice(int quantity, Item item)
        {
            var bestSpecialOffer = item.GetBestSpecialOffer(quantity);

            if (bestSpecialOffer != null)
            {
                return bestSpecialOffer.CalculateItemPrice(quantity, item.Price);
            }

            return quantity * item.Price;

        }
    }
}





