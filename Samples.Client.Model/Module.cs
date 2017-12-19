using JetBrains.Annotations;
using Samples.Client.Model.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Model
{
    [UsedImplicitly]
    class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {            
            dependencyRegistrator.RegisterSingleton<IDataService, DataService>();
            dependencyRegistrator.RegisterSingleton<ILoginService, LoginService>();            
        }
    }    
}
