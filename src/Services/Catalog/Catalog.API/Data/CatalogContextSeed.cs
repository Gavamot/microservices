using AutoBogus;
using Bogus;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Linq;

namespace Catalog.API.Data
{
    class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> products)
        {
            bool existProduct = products.Find(p => true).Any();
            if (existProduct) return;
            var seeds = GenerateProducts();
            products.InsertMany(seeds);
        }

        private static Product[] GenerateProducts()
        {
            return new AutoFaker<Product>()
               .RuleFor(x => x.Id, f => new string(Guid.NewGuid().ToString().Replace("-", "").Take(24).ToArray()))
               .RuleSet("empty", rules =>
               {
                   rules.RuleFor(fake => fake.Id, () => "0");
               })
               .GenerateForever()
               .Take(20)
               .ToArray();
        }
    }
}
