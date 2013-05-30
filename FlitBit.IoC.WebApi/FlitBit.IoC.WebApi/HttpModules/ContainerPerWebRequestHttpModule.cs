using System.Web;

namespace FlitBit.IoC.WebApi.HttpModules
{
    public class ContainerPerWebRequestHttpModule : IHttpModule
    {
        IContainer _container;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += (sender, args) => _container = Container.Current.MakeChildContainer(CreationContextOptions.InheritScope);
            context.EndRequest += (sender, args) => _container.Dispose(); ;
        }

        public void Dispose()
        {
        }
    }
}
