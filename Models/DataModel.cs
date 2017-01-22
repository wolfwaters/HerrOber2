using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace HerrOber2.Models
{
    public class DataModel
    {
        private static DataModel instance;

        private DataModel()
        {
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

        public List<Booking> Bookings { get; set; }

        #region Read / Write

        private string GetFileName()
        {
            string name = Assembly.GetExecutingAssembly().Location;
            name = name.Replace(".exe", ".xml");
            return name;
        }

        public void Load()
        {
            string fileName = GetFileName();
            if (!File.Exists(fileName))
            {
                LoadDefaults();
                Save();
                return;
            }

            string xml = File.ReadAllText(fileName);
            instance = Utils.FromXML<DataModel>(xml);
        }

        public void Save()
        {
            string fileName = GetFileName();
            string xml = Utils.ToXml(this);
            File.WriteAllText(fileName, xml);
        }

        private void LoadDefaults()
        {
            Users = new List<User>
            {
                new User("Carsten", "carsten.koblischke@waters.com"),
                new User("Martin", "martin.kruse@waters.com"),
                new User("Ralf", "ralf.hoffmann@waters.com"),
                new User("Jörg", "joerg.kessenich@waters.com"),
                new User("Wolfgang", "wolfgang.foerster@waters.com"),
                new User("Bettina", "bettina.hohn@waters.com"),
                new User("Sylvia", "sylvia.mueller@waters.com"),
                new User("Hans", "hans.mueller@waters.com"),
                new User("Rolf", "rolf.grigat@waters.com"),
            };

            Restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Meyers",Url = "https://shop.meyer-menue.de", Phone = "0800 150 150 5",
                    Dishes = new List<Dish>
                    {
                        new Dish { Name = "Menü 1", Price = 4.6},
                        new Dish { Name = "Menü 2", Price = 4.6},
                        new Dish { Name = "Menü 3", Price = 4.6},
                        new Dish { Name = "Menü 4", Price = 5.1},
                        new Dish { Name = "Menü 5", Price = 5.3}
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
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Open,
                    PlannedDeliveryDate = new DateTime(2016,05,04, 12,30,00).ToUniversalTime(),
                    UserId = Users[0].Id
                },
                new Order
                {
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 4",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Open,
                    PlannedDeliveryDate = new DateTime(2016,05,03, 12,30,00).ToUniversalTime(),
                    UserId = Users[1].Id
                },
                new Order
                {
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Ordered,
                    PlannedDeliveryDate = new DateTime(2016,05,03, 12,30,00).ToUniversalTime(),
                    UserId = Users[2].Id
                },
                new Order
                {
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 5",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Ordered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserId = Users[3].Id
                },
                new Order
                {
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 1",
                    Price = 4.3,
                    OrderStatus = OrderStatus.Delivered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserId = Users[4].Id
                },
                new Order
                {
                    Id = NewGuid(),
                    Restaurant = "Meyers",
                    DishName = "Menü 2",
                    Price = 5.3,
                    OrderStatus = OrderStatus.Delivered,
                    PlannedDeliveryDate = new DateTime(2016,05,02, 12,30,00).ToUniversalTime(),
                    UserId = Users[5].Id
                }
            };
        }

        public static string NewGuid()
        {
            Guid id = Guid.NewGuid();
            return id.ToString("N").ToLower();
        }

        #endregion Read / Write
    }
}
