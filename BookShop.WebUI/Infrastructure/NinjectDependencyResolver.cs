﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;
using BookShop.Domain.Concrete;
using Moq;




namespace BookShop.WebUI.Infrastructure
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
            kernel.Bind<IBookRepository>().To<EFBookRepository>();
        }



    }
    
    
}