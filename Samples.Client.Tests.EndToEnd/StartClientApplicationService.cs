using System.IO;
using LogoFX.Client.Testing.Contracts;
using Samples.Client.Tests.Contracts;

namespace Samples.Client.Tests.EndToEnd
{
    class StartClientApplicationService : IStartClientApplicationService
    {
        private const string PackageId = "27a966ff-8069-4e40-898a-b715cd3fc922";

        private readonly IStartApplicationService _startApplicationService;

        public StartClientApplicationService(
            IStartApplicationService startApplicationService)
        {
            _startApplicationService = startApplicationService;
        }

        public void StartApplication()
        {                       
            var currentDirectory = Directory.GetCurrentDirectory();
            //TODO: Resolve file path dynamically 
            Directory.SetCurrentDirectory(
                $@"C:\Users\genna\AppData\Local\DevelopmentFiles\{PackageId}VS.Debug_x86.genna");
            _startApplicationService.StartApplication($"{PackageId}_00zbzmmrpqfhc!App"); 
            Directory.SetCurrentDirectory(currentDirectory);
        }
    }    
}
