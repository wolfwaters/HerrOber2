using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Owin;
using System.Web.Http;
using HerrOber2.Controllers;
using System.Web.Http.Cors;

namespace HerrOber2
{
    public class Startup
    {
        // This method is required by Katana:
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = ConfigureWebApi();

            // Use the extension method provided by the WebApi.Owin library:
            app.UseWebApi(webApiConfiguration);
        }


        private HttpConfiguration ConfigureWebApi()
        {
            var config = new HttpConfiguration();     

            //Add formatter to make enums appear as strings in json
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            //--- Add our global message handler
            config.MessageHandlers.Add(new GlobalMessageHandler());

            //--- 
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            return config;
        }
    }
}
