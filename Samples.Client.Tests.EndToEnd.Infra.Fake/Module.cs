using Attest.Testing.Contracts;
using Attest.Testing.Core.FakeData;
using Attest.Testing.EndToEnd;
using Samples.Client.Data.Fake.Shared;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Samples.Client.Tests.EndToEnd.Infra.Fake
{
    class Module : ICompositionModule<IDependencyRegistrator>
    {                
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
            dependencyRegistrator.RegisterSingleton<IStartApplicationService, StartApplicationService.WithFakeProviders>();
            dependencyRegistrator.RegisterSingleton<IBuilderRegistrationService, BuilderRegistrationService>();
            dependencyRegistrator.RegisterBuilders();
        }        
    }
}
