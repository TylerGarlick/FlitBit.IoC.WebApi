﻿using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using FlitBit.Core;

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
            var childContainer = _container.MakeChildContainer(CreationContextOptions.InstanceTracking);
            return new DependencyScope(childContainer);
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

            return default(IEnumerable<object>);
        }

        public void Dispose()
        {
            Util.Dispose(ref _container);
        }

        class DependencyScope : IDependencyScope
        {
            IContainer _container;

            public DependencyScope(IContainer container)
            {
                _container = container;
            }

            public void Dispose()
            {
                Util.Dispose(ref _container);
            }

            public object GetService(Type serviceType)
            {
                if (_container.CanConstruct(serviceType))
                    return _container.CreateInstance(serviceType);

                return default(Type);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                if (_container.CanConstruct(serviceType))
                {
                    var enumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
                    return (IEnumerable<object>)_container.NewUntyped(LifespanTracking.Automatic, enumerable);
                }

                return default(IEnumerable<object>);
            }
        }
    }

}
