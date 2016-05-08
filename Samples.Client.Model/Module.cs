using JetBrains.Annotations;
using Samples.Client.Model.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Model
{
    [UsedImplicitly]
    class Module : ICompositionModule<IIocContainerRegistrator>
    {
        public void RegisterModule(IIocContainerRegistrator iocContainer)
        {            
            iocContainer.RegisterSingleton<IDataService, DataService>();
            iocContainer.RegisterSingleton<ILoginService, LoginService>();
        }
    }    
}
