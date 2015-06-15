using System.Web.Http;

namespace Dashboard
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration cfg)
        {
            cfg.MapHttpAttributeRoutes();
        }
    }
}