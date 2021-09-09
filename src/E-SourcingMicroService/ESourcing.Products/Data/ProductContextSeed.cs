using ESourcing.Products.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESourcing.Products.Data
{
    public class ProductContextSeed
    {
        private static Random random = new Random();

        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetConfigureProducts());
            }
        }

        private static IEnumerable<Product> GetConfigureProducts()
        {
            List<Product> list = new List<Product>();


            for (int i = 0; i < 8; i++)
            {
                list.Add(
                new Product()
                {
                    Name = RandomString(12),
                    Summary = RandomString(50),
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris risus mauris, dapibus eu cursus vel, convallis a augue. Quisque eget ultricies augue. Nulla vitae malesuada libero, ac venenatis eros. Praesent posuere mollis ex vitae pharetra. Nunc faucibus sapien lectus, nec cursus nisl elementum eget. Maecenas et efficitur justo. Fusce pellentesque libero lacus, non rutrum ex cursus et. Nulla suscipit fringilla scelerisque. Morbi pretium eget orci eu interdum.",
                    ImageFile = "",
                    Price=500.00M,
                    Category="poc"
                });
            }

            return list;
        }

        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

        }
    }



}