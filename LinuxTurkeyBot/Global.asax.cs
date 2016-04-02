using System.Web.Http;
using LinuxTurkeyBot.Engine;

namespace LinuxTurkeyBot
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            OptionContainer.Initialize();
        }
    }
}
