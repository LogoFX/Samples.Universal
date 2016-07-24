using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Tests.Steps
{
    class Module : ICompositionModule<IIocContainerRegistrator>
    {
        public void RegisterModule(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterSingleton<GeneralSteps, GeneralSteps>();
            iocContainer.RegisterSingleton<LoginSteps, LoginSteps>();
        }
    }
}
