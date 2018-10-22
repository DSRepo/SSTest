using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SSTest.Core.Interface;
using SSTest.Core.Business;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace SSTest.Core.Utilies
{
    public static class UnityConfig
    {



        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            //container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            container.RegisterType<IProductRepository, ProductRepository>();

            //container.RegisterTypes(AllClasses.FromLoadedAssemblies(), WithMappings.FromMatchingInterface,WithName.Default);
             //DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));
        GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
          

        }

       

    }
}
