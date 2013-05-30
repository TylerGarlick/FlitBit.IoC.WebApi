using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FlitBit.Core;
using FlitBit.Core.Factory;

namespace FlitBit.IoC.WebApi
{
    public class IoCEnabledWebApiControllerFactory : DefaultControllerFactory, IControllerFactory
    {
        readonly IFactory _currentFactory;
        public IoCEnabledWebApiControllerFactory()
        {
            _currentFactory = FactoryProvider.Factory;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");

            if (string.IsNullOrEmpty(controllerName))
                throw new ArgumentException("Controller Name is empty", "controllerName");

            var controllerType = GetControllerType(requestContext, controllerName);
            var controller = CreateInstance(requestContext, controllerType);
            return controller;
        }

        IController CreateInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, string.Format("Controller not found at {0}", requestContext.HttpContext.Request.Path));

            if (!typeof(IController).IsAssignableFrom(controllerType))
                throw new ArgumentException("Controller must inherit from Controller", "controllerType");

            if (_currentFactory.CanConstruct(controllerType))
                return _currentFactory.CreateInstance(controllerType) as IController;

            return default(IController);
        }
    }
}
