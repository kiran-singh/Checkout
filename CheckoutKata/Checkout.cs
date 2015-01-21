namespace CheckoutKata
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Checkout
    {
        public const string KeyApple = "Apple";

        public const string KeyBeans = "Beans";

        public const string KeyCoke = "Coke";

        public const string KeyDeodorant = "Deodorant";

        public const string KeyEgg = "Egg";

        static readonly IDictionary<string, double> PriceDict
                                            = new Dictionary<string, double>
                                                    {
                                                        { KeyApple, 0.3 },
                                                        { KeyBeans, 0.5 },
                                                        { KeyCoke, 1.8 },
                                                        {KeyDeodorant, 2.5},
                                                        {KeyEgg, 1.2},
                                                    };


        private static readonly IDictionary<string[], Tuple<int, double>> ItemCountDiscountDict =
                                new Dictionary<string[], Tuple<int, double>>
                                    {
                                        { new[] { KeyApple }, new Tuple<int, double>(4, 0.2) },
                                        { new[] { KeyDeodorant }, new Tuple<int, double>(2, 0.5) },
                                        { new[] { KeyEgg }, new Tuple<int, double>(3, 0.6) },
                                        { new[] { KeyBeans, KeyCoke, }, new Tuple<int, double>(1, 0.3) },
                                    };

        public double Scan(params string[] items)
        {
            var sum = items.Sum(x => PriceDict[x]);

            ItemCountDiscountDict.ToList().ForEach(
                itemCountDiscount =>
                {
                    var disountedItemsCountList =
                        items.Where(itemCountDiscount.Key.Contains).GroupBy(x => x).Select(x => x.Count()).ToList();

                    var validDiscountCount = disountedItemsCountList.Count >= itemCountDiscount.Key.Count()
                                                 ? disountedItemsCountList.Min()
                                                 : 0;

                    while (validDiscountCount >= itemCountDiscount.Value.Item1)
                    {
                        sum -= itemCountDiscount.Value.Item2;
                        validDiscountCount -= itemCountDiscount.Value.Item1;
                    }
                });

            return sum;
        }
    }
}