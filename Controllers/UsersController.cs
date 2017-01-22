using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HerrOber2.Models;
using System;
using System.Web;

namespace HerrOber2.Controllers
{
    public class UsersController : BaseController
    {
        [HttpGet]
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            foreach (User user in DataModel.Instance.Users)
                user.CalculateBalance();

            return Ok(DataModel.Instance.Users);
        }

        [HttpGet]
        [Route("users({userId})")]
        public IHttpActionResult GetUser(string userId)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(userId));
            if (user == null)
                return NotFound();
            user.CalculateBalance();
            return Ok(user);
        }

        [HttpGet]
        [Route("users({userId})/orders")]
        public IHttpActionResult GetUserOrders(string userId)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(userId));
            if (user == null)
                return NotFound();

            var queryParameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            IEnumerable<Order> result = DataModel.Instance.Orders.Where(x => x.UserId == userId); 

            //Apply filters

            string orderStatus = queryParameters["orderStatus"];
            if (orderStatus != null)
            {
                OrderStatus status = (OrderStatus) Enum.Parse(typeof(OrderStatus), orderStatus);
                result = result.Where(x => x.OrderStatus == status);
            }
            return Ok(result);
        }


        [HttpPut]
        [Route("users")]
        public IHttpActionResult Put([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Invalid user information");
            try
            {
                User existingUser = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(user.Id));
                if (existingUser == null)
                    DataModel.Instance.Users.Add(user);
                else
                {
                    int index = DataModel.Instance.Users.IndexOf(existingUser);
                    DataModel.Instance.Users[index] = user;
                }

                return OkNoContent();
            }
            catch (Exception ex)
            {
                // System.Web.Http.Results
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("users({userId})/transfer")]
        public IHttpActionResult PutTransfer(string userId, [FromUri] string targetId, [FromUri] double amount)
        {
            try
            {
                User user = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(userId));
                if (user == null)
                    return BadRequest("Den Zahler (" + userId + ") kennen wir leider nicht");

                User recipient = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(targetId));
                if (recipient == null)
                    return BadRequest("Den Zahlungsempfänger (" + targetId+ ") kennen wir leider nicht");

                if (userId.Equals(targetId))
                {
                    return BadRequest("Willst Du Dir wirklich selbst etwas bezahlen?");
                }

                if (amount == 0.0)
                {
                    return BadRequest("Big Spender? 0€ können wir hier nicht buchen");
                }

                Booking transfer = new Booking()
                {
                    ActorId = userId,
                    Recipient = targetId,
                    BookingType = BookingType.InternalTransfer,
                    BookingDate = DateTime.UtcNow,
                    Amount = amount
                };

                DataModel.Instance.Bookings.Add(transfer);

                return OkNoContent();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("users({userId})")]
        public IHttpActionResult Delete(string userId)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.Id.Equals(userId));
            if (user == null)
                return NotFound();
            try
            {
                if (user.CalculateBalance() != 0.0)
                {
                    return BadRequest(user.Name + " kann nicht entfernt werden, da der Kontostand "+ user.Balance.ToString("F2") + " € ist. Gleicht das mal erstmal aus ..");
                }
                DataModel.Instance.Users.Remove(user);

                return OkNoContent();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
