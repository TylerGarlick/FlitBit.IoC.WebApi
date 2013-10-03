using System.Web.Http.Dependencies;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitWebApiDependencyResolver : FlitBitWebApiContainerSharedScope, IDependencyResolver
    {
        readonly IContainer _container;

        public FlitBitWebApiDependencyResolver(IContainer container)
            : base(container)
        {
            _container = container;
        }

        public virtual IDependencyScope BeginScope()
        {
            var childContainer = _container.MakeChildContainer(CreationContextOptions.InstanceTracking);
            return new FlitBitWebApiContainerSharedScope(childContainer);
        }
    }
}
