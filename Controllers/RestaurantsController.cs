using HerrOber2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HerrOber2.Controllers
{
    public class RestaurantsController : BaseController
    {
        #region /restaurants
        [Route("restaurants")]
        public IHttpActionResult Get()
        {
            return Ok(DataModel.Instance.Restaurants);
        }

        [HttpPut]
        [Route("restaurants")]
        public IHttpActionResult Put([FromBody] Restaurant restaurant)
        {
            Restaurant existing = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name == restaurant.Name);
            if (existing != null)
            {
                DataModel.Instance.Restaurants.Remove(existing);
            }
            DataModel.Instance.Restaurants.Add(restaurant);

            return OkNoContent();
        }

        #endregion

        #region /restaurants({name})

        [Route("restaurants({restaurantName})")]
        public IHttpActionResult Get(string restaurantName)
        {
            Restaurant restaurant = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name.Equals(restaurantName));
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

       

        [HttpDelete]
        [Route("restaurants({restaurantName})")]
        public IHttpActionResult Delete(string restaurantName)
        {
            Restaurant existing = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name == restaurantName);
            if (existing != null)
            {
                DataModel.Instance.Restaurants.Remove(existing);
                return StatusCode(HttpStatusCode.NoContent);
            }            
            else
            {
                return NotFound();
            }            
        }

        #endregion

        #region /restaurants({name})/dishes
        [HttpGet]
        [Route("restaurants({restaurantName})/dishes")]
        public IHttpActionResult GetDishes(string restaurantName)
        {
            Restaurant restaurant = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name == restaurantName);
            if(restaurant == null)
            {
                return NotFound();
            }
            List < Dish > dishes = restaurant.Dishes;

            return Ok(dishes);
        }

        [HttpPut]
        [Route("restaurants({restaurantName})/dishes")]
        public IHttpActionResult PutDish(string restaurantName, [FromBody]Dish dishToAdd)
        {
            Restaurant restaurant = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name == restaurantName);
            if (restaurant == null)
            {
                return NotFound();
            }
            List<Dish> dishes = restaurant.Dishes;
            if (dishes == null)
            {
                dishes = new List<Dish>();
            }
            Dish existing = dishes.FirstOrDefault(x => x.Name == dishToAdd.Name);
            if(existing != null)
            {
                dishes.Remove(existing);
            }
            dishes.Add(dishToAdd);

            return OkNoContent();
        }
        #endregion

        #region /restaurants({restaurantName})/dishes({dishName})
        [HttpDelete]
        [Route("restaurants({restaurantName})/dishes({dishName})")]
        public IHttpActionResult DeleteDish(string restaurantName, string dishName)
        {
            Restaurant restaurant = DataModel.Instance.Restaurants.FirstOrDefault(x => x.Name == restaurantName);
            if (restaurant == null)
            {
                return NotFound();
            }
            List<Dish> dishes = restaurant.Dishes;
            if (dishes != null)
            {
                Dish existing = dishes.FirstOrDefault(x => x.Name == dishName);
                if (existing != null)
                {
                    dishes.Remove(existing);
                    return OkNoContent();
                }
            }

            return NotFound();
        }
        #endregion
    }
}
