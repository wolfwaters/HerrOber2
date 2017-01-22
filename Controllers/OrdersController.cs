using HerrOber2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

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
            var userId = queryParameters["userId"];
            if (userId != null)
            {
                result = result.Where(x => x.UserId == userId);
            }

            string orderStatus = queryParameters["orderStatus"];
            if (orderStatus != null)
            {
                result = result.Where(x => x.OrderStatus == (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus));
            }

            string plannedDeliveryDate = queryParameters["plannedDeliveryDate"];
            if (plannedDeliveryDate != null)
            {                
                result = result.Where(x => x.PlannedDeliveryDate == JsonConvert.DeserializeObject<DateTime>(plannedDeliveryDate));
            }

            var plannedDeliveryWeek = queryParameters["plannedDeliveryWeek"];
            if (plannedDeliveryWeek != null)
            {
                var date = DateTime.Parse(plannedDeliveryWeek);
                var day1 = date - TimeSpan.FromDays(1);
                var day2 = date + TimeSpan.FromDays(6);
                result = result.Where(x => x.PlannedDeliveryDate > day1 && x.PlannedDeliveryDate < day2);
            }
            return Ok(result);
        }

        [HttpPut]
        [Route("orders")]
        public IHttpActionResult Put([FromBody] Order order)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(order.UserId));
            if (user == null)
                return NotFound();

            var orders = DataModel.Instance.Orders;
            if (string.IsNullOrWhiteSpace(order.Id))
            {
                order.Id = DataModel.NewGuid();
            }
            else
            {
                var existing = orders.FirstOrDefault(x => x.Id == order.Id);
                if (existing != null)
                    orders.Remove(existing);
            }
            orders.Add(order);
            return Ok(order.Id);
        }


        [HttpDelete]
        [Route("orders")]
        public IHttpActionResult Delete()
        {             
            var queryParameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var orderId = queryParameters["orderId"];
            if (orderId != null)
            {
                var orders = DataModel.Instance.Orders;
                var orderToDelete = orders.FirstOrDefault(x => x.Id == orderId);
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
