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
            return Ok(DataModel.Instance.Users);
        }

        [Route("users({emailAddress})")]
        public IHttpActionResult Get(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPut]
        [Route("users")]
        public IHttpActionResult Put([FromBody] User user)
        {
            //User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
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

                return OkNoContent();
            }
            catch (Exception ex)
            {
                // System.Web.Http.Results
                return InternalServerError(ex);
             }
        }

    }
}
