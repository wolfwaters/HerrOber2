using System;

namespace HerrOber2.Models
{
    public class Order
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Restaurant { get; set; }

        public DateTime PlannedDeliveryDate { get; set; }

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
