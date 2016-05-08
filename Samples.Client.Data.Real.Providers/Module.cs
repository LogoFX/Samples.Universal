#if REAL
using JetBrains.Annotations;
using Samples.Client.Data.Contracts.Providers;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Data.Real.Providers
{
    [UsedImplicitly]
    class Module : ICompositionModule<IIocContainerRegistrator>
    {
        public void RegisterModule(IIocContainerRegistrator iocContainer)
        {
            iocContainer.RegisterSingleton<ILoginProvider, LoginProvider>();
            iocContainer.RegisterSingleton<IWarehouseProvider, WarehouseProvider>();            
        }
    }
}
#endif