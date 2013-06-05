using System;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FlitBit.Core;
using FlitBit.Emit;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitHttpControllerActivator : IHttpControllerActivator
    {
        static readonly MethodInfo CreateNewMethod = typeof(FlitBitHttpControllerActivator).MatchGenericMethod("CreateNew", BindingFlags.Instance | BindingFlags.NonPublic, 1, typeof(object));
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var create = CreateNewMethod.MakeGenericMethod(controllerType);
            return create.Invoke(this, new object[] { }) as IHttpController;
        }

        T CreateNew<T>()
        {
            var currentFactory = FactoryProvider.Factory;
            return currentFactory.CanConstruct<T>() ?
                currentFactory.CreateInstance<T>() :
                default(T);
        }
    }
}
