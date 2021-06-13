using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public ShoppingCart(string userName)
        {
            this.UserName = userName;
        }

        public ShoppingCart() { }

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}
