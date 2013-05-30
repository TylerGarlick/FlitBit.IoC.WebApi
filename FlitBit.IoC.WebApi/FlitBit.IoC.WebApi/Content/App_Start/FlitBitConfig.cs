using System.Web.Http;
using System.Web.Http.Dispatcher;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(FlitBit.IoC.WebApi.App_Start.FlitBitConfig), "PreStart")]
namespace FlitBit.IoC.WebApi.App_Start
{
    public static class FlitBitConfig
    {
        public static void PreStart()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new IoCEnabledWebApiControllerFactory());
        }
    }
}
