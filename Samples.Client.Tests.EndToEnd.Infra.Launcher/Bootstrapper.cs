using System.Collections.Generic;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.IoC;
using Solid.Practices.Middleware;

namespace Samples.Client.Tests.EndToEnd.Infra.Launcher
{
    public class Bootstrapper : BootstrapperBase,
        IExtensible<Bootstrapper>,         
        IHaveRegistrator        
    {
        private readonly
            List<IMiddleware<Bootstrapper>>
            _middlewares =
                new List<IMiddleware<Bootstrapper>>();

        public Bootstrapper(IDependencyRegistrator dependencyRegistrator)
        {
            Registrator = dependencyRegistrator;     
        }

        public IDependencyRegistrator Registrator { get; }

        /// <summary>
        /// Extends the functionality by using the specified middleware.
        /// </summary>
        /// <param name="middleware">The middleware.</param>
        /// <returns></returns>
        public Bootstrapper Use(
            IMiddleware<Bootstrapper> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}