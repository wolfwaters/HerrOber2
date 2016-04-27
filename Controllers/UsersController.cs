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
        [Route("users({emailAddress})")]
        public IHttpActionResult GetUser(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            if (user == null)
                return NotFound();
            user.CalculateBalance();
            return Ok(user);
        }

        [HttpGet]
        [Route("users({emailAddress})/orders")]
        public IHttpActionResult GetUserOrders(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            if (user == null)
                return NotFound();

            var queryParameters = HttpUtility.ParseQueryString(Request.RequestUri.Query);

            IEnumerable<Order> result = DataModel.Instance.Orders.Where(x => x.UserEmail == emailAddress); 

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
                User existingUser = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(user.EmailAddress));
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
        [Route("users({emailAddress})/transfer")]
        public IHttpActionResult PutTransfer(string emailAddress, [FromUri] string targetemail, [FromUri] double amount)
        {
            // test: http://localhost:4711/users(martin.kruse@waters.com)/transfer?targetemail=martin.kruse@waters.com&amount=1.0
            try
            {
                User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
                if (user == null)
                    return BadRequest("Den Zahler (" + emailAddress + ") kennen wir leider nicht");

                User recipient = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(targetemail));
                if (recipient == null)
                    return BadRequest("Den Zahlungsempfänger (" + targetemail+ ") kennen wir leider nicht");

                if (emailAddress.Equals(targetemail))
                {
                    return BadRequest("Willst Du Dir wirklich selbst etwas bezahlen?");
                }

                if (amount == 0.0)
                {
                    return BadRequest("Big Spender? 0€ können wir hier nicht buchen");
                }

                Booking transfer = new Booking()
                {
                    ActorEmail = emailAddress,
                    Recipient = targetemail,
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
        [Route("users({emailAddress})")]
        public IHttpActionResult Delete(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            if (user == null)
                return NotFound();
            try
            {
                if (user.CalculateBalance() != 0.0)
                {
                    return BadRequest(user.DisplayName + " kann nicht entfernt werden, da der Kontostand "+ user.Balance.ToString("F2") + " € ist. Gleicht das mal erstmal aus ..");
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
