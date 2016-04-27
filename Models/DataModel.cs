using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerrOber2.Models
{
    public class DataModel
    {
        private static DataModel instance;

        private DataModel()
        {
            Users = new List<User>() {
                new User("Carsten", "carsten.koblischke@waters.com"),
                new User("Martin", "martin.kruse@waters.com"),
                new User("Ralf", "ralf.hoffmann@waters.com"),
                new User("Wolfgang", "wolfgang.foerster@waters.com")
            };

            Restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Meyers",Url = "https://shop.meyer-menue.de", Phone = "0800 150 150 5",
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Menü 1", Price = 4.5},
                        new Dish { Name = "Menü 2", Price = 5.0}
                    }
                },
                new Restaurant
                {
                    Name = "China Imbiss Lucky Tree", Url = "http://www.lucky-tree.de/", Phone = "02234-9885085",
                    Dishes = new List<Dish>()
                }
            };

            Orders = new List<Order>
            {
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Open,
                    PlannedDeliveryDate = new DateTime(2016,05,04, 12,30,00).ToUniversalTime(),
                    UserEmail = "ralf.hoffmann@waters.com"
                },
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 4",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Open,
                    PlannedDeliveryDate = new DateTime(2016,05,03, 12,30,00).ToUniversalTime(),
                    UserEmail = "martin.kruse@waters.com"
                },
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Ordered,
                    PlannedDeliveryDate = new DateTime(2016,05,03, 12,30,00).ToUniversalTime(),
                    UserEmail = "ralf.hoffmann@waters.com"
                },
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 5",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Ordered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserEmail = "wolfgang.foerster@waters.com"
                },
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Delivered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserEmail = "carsten.koblischke@waters.com"
                },
                new Order
                {
                    Restaurant = "Meyers",
                    DishName = "Menü 2",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Delivered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserEmail = "wolfgang.foerster@waters.com"
                }
            };
        }

        public static DataModel Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataModel();

                return instance;
            }
        }

        public List<User> Users { get; set; }

        public List<Restaurant> Restaurants { get; set; }

        public List<Order> Orders { get; set; }

        #region Read / Write

        public bool ReadFromFile(string fileName)
        {
            xmlFileName = "";
            if (!File.Exists(xmlFileName))
                return false;

            xmlFileName = fileName;
            return true;
        }
        string xmlFileName;

        public bool Save()
        {
            if (!File.Exists(xmlFileName))
                return false;

            return true;
        }

        #endregion Read / Write
    }
}
