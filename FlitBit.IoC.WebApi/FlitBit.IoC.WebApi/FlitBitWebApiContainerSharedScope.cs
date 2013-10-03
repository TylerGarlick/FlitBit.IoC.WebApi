using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using FlitBit.Core;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitWebApiContainerSharedScope : IDependencyScope
    {
        IContainer _container;

        public FlitBitWebApiContainerSharedScope(IContainer container)
        {
            _container = container;
        }

        public virtual object GetService(Type serviceType)
        {
            if (_container.CanConstruct(serviceType))
                return FactoryProvider.Factory.CreateInstance(serviceType);

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
            Util.Dispose(ref _container);
        }
    }
}