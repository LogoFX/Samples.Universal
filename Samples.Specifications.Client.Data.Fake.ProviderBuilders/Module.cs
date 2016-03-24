using System.Composition;
using Attest.Fake.Builders;
using Attest.Fake.Moq;
using Solid.Practices.Modularity;

namespace LogoFX.Samples.Specifications.Client.Data.Fake.ProviderBuilders
{
    [Export(typeof(ICompositionModule))]
    class Module : IPlainCompositionModule
    {
        public void RegisterModule()
        {
            FakeFactoryContext.Current = new FakeFactory();
        }
    }
}
