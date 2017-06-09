using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop2.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public string Status { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
