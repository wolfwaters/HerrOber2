using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HerrOber2.Models;

namespace HerrOber2.Controllers
{
    public class GlobalMessageHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                string method = request.Method.ToString();
                if (method != "GET")
                    DataModel.Instance.Save();
            }
            return response;
        }
    }
}
