using BeFaster.App.Solutions.TST;
using Newtonsoft.Json.Linq;
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
    // Round 3
    //+------+-------+------------------------+
    //| Item | Price | Special offers         |
    //+------+-------+------------------------+
    //| A    | 50    | 3A for 130, 5A for 200 |
    //| B    | 30    | 2B for 45              |
    //| C    | 20    |                        |
    //| D    | 15    |                        |
    //| E    | 40    | 2E get one B free      |
    //| F    | 10    | 2F get one F free      |
    //+------+-------+------------------------+
    // Round 4
    //+------+-------+------------------------+
    //| Item | Price | Special offers         |
    //+------+-------+------------------------+
    //| A    | 50    | 3A for 130, 5A for 200 |
    //| B    | 30    | 2B for 45              |
    //| C    | 20    |                        |
    //| D    | 15    |                        |
    //| E    | 40    | 2E get one B free      |
    //| F    | 10    | 2F get one F free      |
    //| G    | 20    |                        |
    //| H    | 10    | 5H for 45, 10H for 80  |
    //| I    | 35    |                        |
    //| J    | 60    |                        |
    //| K    | 80    | 2K for 150             |
    //| L    | 90    |                        |
    //| M    | 15    |                        |
    //| N    | 40    | 3N get one M free      |
    //| O    | 10    |                        |
    //| P    | 50    | 5P for 200             |
    //| Q    | 30    | 3Q for 80              |
    //| R    | 50    | 3R get one Q free      |
    //| S    | 30    |                        |
    //| T    | 20    |                        |
    //| U    | 40    | 3U get one U free      |
    //| V    | 50    | 2V for 90, 3V for 130  |
    //| W    | 20    |                        |
    //| X    | 90    |                        |
    //| Y    | 10    |                        |
    //| Z    | 50    |                        |
    //+------+-------+------------------------+

    public class Item
    {
        public Item(int price, List<SpecialOffer> specialOffers = null)
        {
            this.Price = price;
            this.SpecialOffers = specialOffers ?? new List<SpecialOffer>();
            this.SpecialOffers = this.SpecialOffers.OrderByDescending(x => x.Quantity).ToList();
        }

        public int Price { get; }

        public List<SpecialOffer> SpecialOffers { get; } //Replace by SortedSet ordered by quantity descending

        public int CalculatePrice(int quantity)
        {
            var totalPrice = quantity * this.Price;
            var missingQuantity = quantity;

            foreach (var specialOffer in this.SpecialOffers)
            {
                if (!specialOffer.Price.HasValue)
                {
                    continue;
                }
                if (missingQuantity == 0)
                {
                    return totalPrice;
                }

                var discount = specialOffer.DiscountOffer(missingQuantity, this.Price);

                totalPrice -= discount;

                if (discount > 0)
                {
                    missingQuantity -= specialOffer.Quantity * (missingQuantity / specialOffer.Quantity);
                }
            }

            return totalPrice;
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

        public int DiscountOffer(int quantity, int unitPrice)
        {
            if (this.Price != null && quantity >= this.Quantity)
            {
                var offerMultiplier = quantity / this.Quantity;

                return (offerMultiplier * this.Quantity * unitPrice) - (offerMultiplier * this.Price.Value);
            }

            return 0;
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
            { 'E', new Item(40, new List<SpecialOffer>{ new SpecialOffer(2, null, 'B') }) },
            { 'F', new Item(10, new List<SpecialOffer>{ new SpecialOffer(2, null, 'F') }) },
            { 'G', new Item(20) },
            { 'H', new Item(10) },
            { 'I', new Item(35) },
            { 'J', new Item(60) },
            { 'K', new Item(80) },
            { 'L', new Item(90) },
            { 'M', new Item(15) },
            { 'N', new Item(40) },
            { 'O', new Item(10) },
            { 'P', new Item(50) },
            { 'Q', new Item(30) },
            { 'R', new Item(50) },
            { 'S', new Item(30) },
            { 'T', new Item(20) },
            { 'U', new Item(40) },
            { 'V', new Item(50) },
            { 'W', new Item(20) },
            { 'X', new Item(90) },
            { 'Y', new Item(10) },
            { 'Z', new Item(50) },
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

                totalPrice += item.CalculatePrice(sku.Value);

                totalPrice -= CalculateDiscount(skuQuantities, sku, item);
            }

            return totalPrice;
        }

        private static bool IsCapitalLetter(char c)
        {
            return c >= 65 && c <= 90;
        }

        private static int CalculateDiscount(Dictionary<char, int> skuQuantities, KeyValuePair<char, int> sku, Item item)
        {
            var specialOffer = item.GetSpecialOfferForOtherItems(sku.Value);
            if (specialOffer?.Item == null
                || !prices.TryGetValue(specialOffer.Item.Value, out var offeredItem)
                || !skuQuantities.TryGetValue(specialOffer.Item.Value, out var offeredItemQuantity))
            {
                return 0;
            }

            var amountOfSpecialOffersAvailable = sku.Value / specialOffer.Quantity;

            if (sku.Key == specialOffer.Item.Value)
            {
                amountOfSpecialOffersAvailable = sku.Value / (specialOffer.Quantity + 1);
            }

            var newOfferedItemQuantity = Math.Max(offeredItemQuantity - amountOfSpecialOffersAvailable, 0);

            if (newOfferedItemQuantity < sku.Value)
            {

            }

            var offeredItemTotalPrice = offeredItem.CalculatePrice(offeredItemQuantity);
            var newOfferedItemTotalPrice = offeredItem.CalculatePrice(newOfferedItemQuantity);

            if (newOfferedItemTotalPrice < offeredItemTotalPrice)
            {
                return offeredItemTotalPrice - newOfferedItemTotalPrice;
            }

            return 0;
        }
    }
}

