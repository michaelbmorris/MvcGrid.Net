using System;
using System.Web;
using MvcGrid.Web.App_Start;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MVCGrid.Web.Models;
using Ninject;

[assembly:
    WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly:
    WebActivatorEx.ApplicationShutdownMethodAttribute(
        typeof(NinjectWebCommon),
        "Stop")]

namespace MvcGrid.Web.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(
                typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>()
                    .ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>()
                    .To<HttpApplicationInitializationHttpModule>();

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
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPersonRepository>().To<PersonRepository>();
        }
    }
}