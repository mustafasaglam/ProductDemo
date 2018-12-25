using Autofac;
using Autofac.Integration.Mvc;
using ProductDemo.Core.Infrastructure;
using ProductDemo.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductDemo.Web
{
    public static class Bootstrapper
    {
        public static void RunConfig()
        {
            BuildAutoFac();
        }

        private static void BuildAutoFac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);//Conrollerları buildera kayıt etme

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<ProductFeatureRepository>().As<IProductFeatureRepository>();
            builder.RegisterType<ProductImagerepository>().As<IProductImageRepository>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}