using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerrOber2.Models
{
    public class Restaurant
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string EMail { get; set; }

        public string Url { get; set; }

        public List<Dish> Dishes { get; set; }
    }
}
