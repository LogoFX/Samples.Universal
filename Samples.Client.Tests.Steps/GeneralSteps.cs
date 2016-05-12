using Attest.Testing.Core;
using LogoFX.Client.Testing.Contracts;

namespace Samples.Client.Tests.Steps
{
    public static class GeneralSteps
    {
        private static readonly IStartApplicationService _startApplicationService =
            ScenarioHelper.Get<IStartApplicationService>();

        public static void WhenIOpenTheApplication()
        {            
            //TODO: add support for E2E if needed
            _startApplicationService.StartApplication(string.Empty);
        }
    }
}
