using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParm)
        {
            _kernel = kernelParm;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            // add bindings here

            _kernel.Bind<IProductRepository>().To<EFProductRepository>();

            // mock repo
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(
            //    new List<Product>
            //    {
            //        new Product{Name = "Football", Price = 25},
            //        new Product{Name = "Surf board", Price = 179},
            //        new Product{Name = "Running shoes", Price = 95}
            //    });

            // want Ninject to return the same mock object whenever it gets a request - a singleton (using .ToConstant())
            // _kernel.Bind<IProductRepository>().ToConstant(mock.Object);
        }
    }
}