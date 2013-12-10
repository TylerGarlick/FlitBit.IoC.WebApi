using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using FlitBit.Core;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitSharedDependencyScope : IDependencyScope
    {
        IContainer _childContainer;

        public FlitBitSharedDependencyScope(IContainer childContainer)
        {
            _childContainer = childContainer;
        }

        public void Dispose()
        {
            Util.Dispose(ref _childContainer);
        }

        public object GetService(Type serviceType)
        {
            if (_childContainer.CanConstruct(serviceType))
                return _childContainer.CreateInstance(serviceType);

            return default(Type);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (_childContainer.CanConstruct(serviceType))
            {
                var enumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
                return (IEnumerable<object>)_childContainer.NewUntyped(LifespanTracking.Automatic, enumerable);
            }

            return Enumerable.Empty<object>();
        }
    }
}