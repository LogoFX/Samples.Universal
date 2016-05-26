using Attest.Fake.Registration;
using Attest.Testing.Core;
using Caliburn.Micro;
using LogoFX.Client.Testing.Integration;
using Samples.Client.Data.Fake.ProviderBuilders;
using Samples.Universal.Client.Presentation.Shell.ViewModels;
using Samples.Universal.Client.Tests.Integration.Infra.Core;

namespace Samples.Universal.Client.Tests.Integration.Infra.Shared
{
    public class StartApplicationService : StartApplicationServiceBase
    {
        // ReSharper disable once RedundantOverridenMember
        protected override void RegisterFakes()
        {
            base.RegisterFakes();            
            RegistrationHelper.RegisterBuilder(ScenarioHelper.Registrator, ScenarioHelper.GetOrCreate(WarehouseProviderBuilder.CreateBuilder));
        }

        protected override void OnStart(object rootObject)
        {
            var shell = (ShellViewModel)rootObject;
            StructureHelper.SetRootObject(shell);
            ScreenExtensions.TryActivate(shell);
            ScreenExtensions.TryActivate(StructureHelper.GetLogin());
        }
    }
}