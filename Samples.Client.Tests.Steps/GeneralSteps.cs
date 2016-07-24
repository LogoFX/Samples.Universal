using LogoFX.Client.Testing.Contracts;

namespace Samples.Client.Tests.Steps
{
    public class GeneralSteps
    {
        private readonly IStartApplicationService _startApplicationService;

        public GeneralSteps(IStartApplicationService startApplicationService)
        {
            _startApplicationService = startApplicationService;
        }

        public void WhenIOpenTheApplication()
        {            
            //TODO: add support for E2E if needed
            _startApplicationService.StartApplication(string.Empty);
        }
    }
}
