using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HerrOber2.Controllers
{
    public class BaseController : ApiController
    {
        protected IHttpActionResult OkNoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
