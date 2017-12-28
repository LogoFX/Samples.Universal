using LogoFX.Client.Testing.Contracts;
using Samples.Client.Tests.Contracts;

namespace Samples.Client.Tests.EndToEnd
{
    class StartClientApplicationService : IStartClientApplicationService
    {        
        private readonly IStartApplicationService _startApplicationService;

        public StartClientApplicationService(
            IStartApplicationService startApplicationService)
        {
            _startApplicationService = startApplicationService;
        }

        public void StartApplication()
        {                        
            _startApplicationService.StartApplication("27a966ff-8069-4e40-898a-b715cd3fc922_00zbzmmrpqfhc!App");            
        }
    }    
}
