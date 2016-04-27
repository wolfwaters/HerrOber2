using HerrOber2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace HerrOber2.Controllers
{
    public class OrdersController : BaseController
    {
        #region /orders
        [HttpGet]
        [Route("orders")]
        public IHttpActionResult GetOrders()
        {
            var queryParameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            List<Order> orders = DataModel.Instance.Orders;

            IEnumerable<Order> result = orders;

            //Apply filters

            string userEmail = queryParameters["userEmail"];

            if (userEmail != null)
            {
                result = result.Where(x => x.UserEmail == userEmail);
            }


            string orderStatus = queryParameters["orderStatus"];

            if (orderStatus != null)
            {
                result = result.Where(x => x.OrderStatus == (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus));
            }

            string plannedDeliveryDate = queryParameters["plannedDeliveryDate"];


            if (plannedDeliveryDate != null)
            {                
                result = result.Where(x => x.PlannedDeliveryDate == Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(plannedDeliveryDate));
            } 
            return Ok(result);
        }

        [HttpPut]
        [Route("orders")]
        public IHttpActionResult Put([FromBody] Order order)
        {
            List<Order> orders = DataModel.Instance.Orders;
            Order existing = orders.FirstOrDefault(x =>
                x.UserEmail == order.UserEmail
                && x.DishName == order.DishName
                && x.PlannedDeliveryDate == order.PlannedDeliveryDate
                && x.Restaurant == order.Restaurant
                );
            if (existing != null)
            {
                //Delete it
                orders.Remove(existing);
            }
            orders.Add(order);

            return OkNoContent();
        }


        [HttpDelete]
        [Route("orders")]
        public IHttpActionResult Delete()
        {             
            List<Order> orders = DataModel.Instance.Orders;

            //Apply filters
            var queryParameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            string userEmail = queryParameters["userEmail"];
            string restaurant = queryParameters["restaurant"];
            string plannedDeliveryDate = queryParameters["plannedDeliveryDate"];
            string dishName = queryParameters["dishName"];
            if (userEmail != null && restaurant != null && plannedDeliveryDate != null && dishName != null)
            {
                Order orderToDelete = orders.FirstOrDefault(x => x.UserEmail == userEmail
                    && x.Restaurant == restaurant
                    && x.DishName == dishName
                    && x.PlannedDeliveryDate == Newtonsoft.Json.JsonConvert.DeserializeObject<DateTime>(plannedDeliveryDate)
                );

                if (orderToDelete != null)
                {
                    orders.Remove(orderToDelete);
                    return OkNoContent();
                } 
                else
                {
                    return NotFound();
                }              
            }


            return StatusCode(System.Net.HttpStatusCode.NotAcceptable);
        }


        #endregion
    }
}
