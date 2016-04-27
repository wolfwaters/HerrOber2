using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using HerrOber2.Models;

namespace HerrOber2.Controllers
{
    public class UsersController : ApiController
    {
        [Route("users")]
        public IEnumerable<User> Get()
        {
            return DataModel.Instance.Users;
        }

        [Route("users({emailAddress})")]
        public User Get(string emailAddress)
        {
            User user = DataModel.Instance.Users.FirstOrDefault(x => x.EmailAddress.Equals(emailAddress));
            //if (user == null)
                //return NotFound();
            return user;
        }
    }
}
