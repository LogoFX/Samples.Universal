using Attest.Fake.Builders;
using Attest.Testing.xUnit;
using LogoFX.Client.Testing.Contracts;

namespace Samples.Universal.Client.Tests.Integration.Infra
{
    public class BuilderRegistrationService : StepsBase, IBuilderRegistrationService
    {
        void IBuilderRegistrationService.RegisterBuilder<TService>(FakeBuilderBase<TService> builder)
        {
            this.RegisterBuilder<TService>(builder);
        }
    }
}