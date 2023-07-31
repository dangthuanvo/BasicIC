[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BasicIC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BasicIC.App_Start.NinjectWebCommon), "Stop")]

namespace BasicIC.App_Start
{
    using AutoMapper;
    using global::Common.Commons;
    using global::Common.Interfaces;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Repository.EF;
    using Repository.Interfaces;
    using Repository.Repositories;
    using BasicIC.Config;
    using System;
    using System.Web;
    using BasicIC.Interfaces;
    using Settings.Config;
    using BasicIC.Services.Interfaces;
    using BasicIC.Services.Implement;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Inject common
            kernel.Bind<ILogger>().To<Logger>();

            kernel.Bind<IConfigManager>().To<ConfigManager>();

            kernel.Bind<IMapper>().ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<MappingProfile>();
                    cfg.ConstructServicesUsing(t => kernel.Get(t));
                });
                return config.CreateMapper();
            }).InSingletonScope();

            kernel.Bind(typeof(IRepositorySql<>)).To(typeof(BaseRepositorySql<>));
            kernel.Bind(typeof(IRepositorySql<>)).To(typeof(BasicICRepository<>));

            kernel.Bind<ICustomerService>().To<CustomerService>();

            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<IImageService>().To<ImageService>();
            kernel.Bind<IProductAttributeService>().To<ProductAttributeService>();
            kernel.Bind<IAttributeService>().To<AttributeService>();    
        }
    }
}