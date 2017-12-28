using Samples.Client.Tests.Contracts;
using Solid.Practices.IoC;

namespace Samples.Client.Tests.EndToEnd.Infra.Launcher
{
    public static class Extensions
    {
        public static void Initialize(this IIocContainer iocContainer)
        {
            new Startup(iocContainer).Initialize();
        }

        public static void Setup(this IIocContainer iocContainer)
        {
            var setupService = iocContainer.Resolve<ISetupService>();
            setupService.Setup();
        }
        
        public static void Teardown(this IIocContainer iocContainer)
        {
            var teardownService = iocContainer.Resolve<ITeardownService>();
            teardownService.Teardown();
        }
    }
}