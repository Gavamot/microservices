using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Rep
{
    public class ProductRep : IProductRep
    {
        private readonly ICatalogContext context;

        public ProductRep(ICatalogContext context)
        {
            this.context = context;
        }

        public Task CreateProduct(Product product)
        {
            return context.Products.InsertOneAsync(product);
        }

        public async Task<bool> deleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var res = await context.Products.DeleteOneAsync(filter);
            return res.IsAcknowledged && res.DeletedCount > 0;
        }

        public Task<Product> GetProduct(string id)
        {
            return context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> getProductByCategory(string categoryName)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Category, categoryName);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.ElemMatch(p => p.Name, name);
            return await context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var res = await context.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return res.IsAcknowledged && res.ModifiedCount > 0;
        }
    }

    public interface IProductRep
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> getProductByCategory(string categoryName);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> deleteProduct(string id);
    }
}
