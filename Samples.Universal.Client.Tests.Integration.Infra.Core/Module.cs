using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Tests.Integration.Infra.Core
{
    class Module : ICompositionModule<IIocContainerRegistrator>
    {
        public void RegisterModule(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterSingleton<StructureHelper, StructureHelper>();
        }
    }
}