using Solid.Bootstrapping;
using Solid.Practices.IoC;

namespace Samples.Client.Tests.EndToEnd.Infra.Launcher
{    
    class Startup
    {
        private readonly IIocContainer _iocContainer;

        public Startup(IIocContainer iocContainer)
        {
            _iocContainer = iocContainer;                   
        }

        public void Initialize()
        {
            var bootstrapper =
                new Bootstrapper(_iocContainer)
                    .Use(new RegisterCompositionModulesMiddleware<Bootstrapper>());            
            bootstrapper.Initialize();
        }
    }
}
