using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using FlitBit.Core;

namespace FlitBit.IoC.WebApi
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        IContainer ChildContainer { get; set; }
        ContainerSharedScope SharedContainer { get; set; }

        public DefaultDependencyResolver()
        {
            ChildContainer = Container.Current.MakeChildContainer(CreationContextOptions.InstanceTracking | CreationContextOptions.InheritScope);
            SharedContainer = new ContainerSharedScope(ChildContainer);
        }

        public void Dispose()
        {
            SharedContainer.Dispose();

            if (!ChildContainer.IsDisposed)
                ChildContainer.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return SharedContainer.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return SharedContainer.GetServices(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return SharedContainer;
        }

        private sealed class ContainerSharedScope : IDependencyScope
        {
            IContainer Container { get; set; }

            public ContainerSharedScope(IContainer container)
            {
                Container = container.MakeChildContainer(CreationContextOptions.InheritScope | CreationContextOptions.InstanceTracking);
            }

            public object GetService(Type serviceType)
            {
                if (Container.CanConstruct(serviceType))
                    return FactoryProvider.Factory.CreateInstance(serviceType);

                return default(Type);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                if (Container.CanConstruct(serviceType))
                    return (IEnumerable<object>)FactoryProvider.Factory.CreateInstance(serviceType);

                return Enumerable.Empty<object>();
            }

            public void Dispose()
            {
                if (!Container.IsDisposed)
                    Container.Dispose();
            }
        }
    }
}
