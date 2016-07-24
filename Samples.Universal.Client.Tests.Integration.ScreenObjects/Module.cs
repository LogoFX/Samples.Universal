using Samples.Client.Tests.Contracts.ScreenObjects;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Tests.Integration.ScreenObjects
{
    class Module : ICompositionModule<IIocContainerRegistrator>
    {
        public void RegisterModule(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterSingleton<ILoginScreenObject, LoginScreenObject>();
            iocContainer.RegisterSingleton<IMainScreenObject, MainScreenObject>();
        }
    }
}
