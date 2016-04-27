using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HerrOber2.Models;
using System;

namespace HerrOber2.Controllers
{
    public class UsersController : BaseController
    {
        [Route("users")]
        public IHttpActionResult Get()
        {
            foreach (User user in DataModel.Instance.Users)
                user.CalculateBalance();

            return Ok(DataModel.Instance.Users);
        }

        [Route("users({emailAddress})")]
        public IHttpActionResult Get(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            if (user == null)
                return NotFound();
            user.CalculateBalance();
            return Ok(user);
        }


        [HttpPut]
        [Route("users")]
        public IHttpActionResult Put([FromBody] User user)
        {
            if (user == null)
                return BadRequest();
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
                //TODO: DataModel.Save

                return OkNoContent();
            }
            catch (Exception ex)
            {
                // System.Web.Http.Results
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
                    return BadRequest(user.DisplayName + " kann nicht gelöscht werden, da der Kontostand "+ user.Balance.ToString() + " € ist");
                }
                DataModel.Instance.Users.Remove(user);

                //TODO: DataModel.Save

                return OkNoContent();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }

    }
}
