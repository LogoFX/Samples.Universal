using Attest.Testing.Core;
using LogoFX.Client.Testing.Contracts;
using Samples.Client.Tests.Contracts.ScreenObjects;
using Samples.Universal.Client.Tests.Integration.ScreenObjects;

namespace Samples.Universal.Client.Tests.Integration.Infra.Shared
{
    public static class ServiceRegistrationHelper
    {
        public static void RegisterIntegrationObjects()
        {
            ScenarioHelper.Add(new StartApplicationService(), typeof(IStartApplicationService));
            ScenarioHelper.Add(new LoginScreenObject(), typeof(ILoginScreenObject));
            ScenarioHelper.Add(new MainScreenObject(), typeof (IMainScreenObject));
        }
    }
}
