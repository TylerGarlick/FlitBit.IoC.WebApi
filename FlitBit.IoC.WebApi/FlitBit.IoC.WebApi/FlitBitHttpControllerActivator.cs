using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using FlitBit.Core;

namespace FlitBit.IoC.WebApi
{
    public class FlitBitHttpControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var currentFactory = FactoryProvider.Factory;

            if (currentFactory.CanConstruct(controllerType))
                return currentFactory.CreateInstance(controllerType) as IHttpController;
            
            return default(IHttpController);
        }
    }
}
