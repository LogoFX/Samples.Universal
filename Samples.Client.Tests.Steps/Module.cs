using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Tests.Steps
{
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator.RegisterSingleton<GeneralSteps, GeneralSteps>();
            dependencyRegistrator.RegisterSingleton<LoginSteps, LoginSteps>();
            dependencyRegistrator.RegisterSingleton<GivenLoginSteps, GivenLoginSteps>();
            dependencyRegistrator.RegisterSingleton<MainSteps, MainSteps>();
        }
    }
}
