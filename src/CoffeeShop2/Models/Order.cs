using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShop2.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public double Total { get; set; }
        public string OrderStatus { get; set; }
        public DateTime ShipDate { get; set; }
        public string TrackShipping { get; set; }

    }
}
