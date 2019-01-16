using Attest.Fake.Core;
using Attest.Fake.Moq;
using Solid.Practices.Modularity;

namespace Samples.Client.Data.Fake.ProviderBuilders
{
    class Module : IPlainCompositionModule
    {
        public void RegisterModule()
        {
            FakeFactoryContext.Current = new FakeFactory();
            ConstraintFactoryContext.Current = new ConstraintFactory();
        }
    }
}