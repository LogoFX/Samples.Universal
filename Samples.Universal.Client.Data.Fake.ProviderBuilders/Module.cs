using Attest.Fake.Builders;
using Attest.Fake.Moq;
using Solid.Practices.Modularity;

namespace Samples.Universal.Client.Data.Fake.ProviderBuilders
{    
    class Module : IPlainCompositionModule
    {
        public void RegisterModule()
        {
            FakeFactoryContext.Current = new FakeFactory();
        }
    }
}
