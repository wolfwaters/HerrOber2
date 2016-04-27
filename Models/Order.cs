using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerrOber2.Models
{
    public class Order
    {
        public string Restaurant { get; set; }

        public DateTime PlannedDeliveryDate { get; set; }

        public string UserEmail { get; set; }

        public string DishName { get; set; }

        public double Price { get; set; }

        public OrderStatus OrderStatus { get; set; }
    }

    public enum OrderStatus
    {
        Open,
        Ordered,
        Delivered
    }
}
