using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using MerchendisersMVC.Domain.DAL.Abstract;
using MerchendisersMVC.Domain.DAL;
using MerchendisersMVC.Domain.Models;

namespace MerchendisersMVC.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        
        private void AddBindings()
        {
            kernel.Bind<IMerchendiserRepository>().To<MerchendiserRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument("context", new MerchendiserDbContext());
            kernel.Bind<ITownRepository>().To<TownRepository>();
        }
    }
}