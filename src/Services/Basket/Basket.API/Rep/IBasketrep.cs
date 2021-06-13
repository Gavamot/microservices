using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Rep
{
    public interface IBasketRep
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }

    public class BasketRep : IBasketRep
    {
        readonly IDistributedCache redis;
        public BasketRep(IDistributedCache redis)
        {
            this.redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public Task DeleteBasket(string userName)
        {
            return redis.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await redis.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var json = JsonConvert.SerializeObject(basket);
            await redis.SetStringAsync(basket.UserName, json);
            return await GetBasket(basket.UserName);
        }
    }
}
