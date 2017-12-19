using System.Reflection;
using LogoFX.Client.Testing.Contracts;
using Samples.Client.Tests.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Tests.EndToEnd
{
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator iocContainer)
        {
            iocContainer.RegisterAutomagically(Assembly.LoadFrom("Samples.Client.Tests.Contracts.dll"),
                Assembly.GetExecutingAssembly());
            iocContainer.RegisterSingleton<IExecutableContainer, ExecutableContainer>();
            iocContainer.RegisterSingleton<StructureHelper, StructureHelper>();
            iocContainer.RegisterSingleton<IApplicationFacade, ApplicationFacade>();
        }
    }
}
