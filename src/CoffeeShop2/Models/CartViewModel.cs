using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop2.Models
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public List<CartItem> Cartitems { get; set; }
        public List<Product> Products { get; set; }
    }
}
