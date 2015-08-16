[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MarkupConverterWeb.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MarkupConverterWeb.App_Start.NinjectWebCommon), "Stop")]

namespace MarkupConverterWeb.App_Start
{
    using MarkupConverterServiceApi;
    using MarkupConverterServiceLocal;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Extensions.Conventions;
    using Ninject.Extensions.Conventions.BindingGenerators;
    using Ninject.Web.Common;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
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

                // Unfortunately we need a direct reference to MarkupConverterServiceLocal because ASP.NET MVC functions as the composition root.
                kernel.Load(Assembly.GetAssembly(typeof(MarkupConverterServiceLocal)));

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
            // Search the plugins directory.
            kernel.Bind(x => x
                .FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .SelectAllClasses()
                .BindDefaultInterface());
        }
    }
}
