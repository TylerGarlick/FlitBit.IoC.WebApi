﻿using System.Web.Http;
using System.Web.Http.Dispatcher;
using $rootnamespace$.App_Start;
using FlitBit.IoC.WebApi;
using FlitBit.IoC;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof($rootnamespace$.App_Start.FlitBitConfig), "PreStart")]
namespace $rootnamespace$.App_Start
{
    public static class FlitBitConfig
    {
        public static void PreStart()
        {
			GlobalConfiguration.Configuration.DependencyResolver = new FlitBitWebApiDependencyResolver(Container.Current);
        }
    }
}
