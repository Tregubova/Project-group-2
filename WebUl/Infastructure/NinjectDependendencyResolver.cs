using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using Domain.Abstract;
using Domain.Entities;
using Moq;
using Ninject;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace WebUl.Infastructure
{
    public class NinjectDependendencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependendencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            Mock<IBookRepository> mock=new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book {Name = "Анна Каренина", Author = "Лев Толстой", Price = 2000},
                new Book {Name = "Алиса в Стране чудес", Author = " Льюис Кэрролл", Price = 1170},
                new Book {Name = "В дороге", Author = "Джек Керуак", Price = 1179}

            });
            kernel.Bind<IBookRepository>().ToConstant(mock.Object);


        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);

        }

        public IEnumerable<object> GetServices(Type ServiceType)
        {
            return kernel.GetAll(ServiceType);
        }


    }
}