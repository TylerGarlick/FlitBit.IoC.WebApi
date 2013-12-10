using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using FlitBit.IoC.Web.Common;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitWebApiDependencyResolver : IDependencyResolver
    {
        IContainer _container;

        public FlitBitWebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public virtual IDependencyScope BeginScope()
        {
            var container = ContainerHelpers.Current ?? _container.MakeChildContainer(CreationContextOptions.InstanceTracking);
            var childContainer = container.MakeChildContainer(CreationContextOptions.InstanceTracking);
            return new FlitBitSharedDependencyScope(childContainer);
        }

        public virtual object GetService(Type serviceType)
        {
            if (_container.CanConstruct(serviceType))
                return _container.CreateInstance(serviceType);

            return default(Type);
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            if (_container.CanConstruct(serviceType))
            {
                var enumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
                return (IEnumerable<object>)_container.NewUntyped(LifespanTracking.Automatic, enumerable);
            }

            return Enumerable.Empty<object>();
        }

        public void Dispose()
        {

        }
    }
}
