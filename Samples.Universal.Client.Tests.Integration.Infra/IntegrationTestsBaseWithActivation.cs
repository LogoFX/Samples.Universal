using Attest.Testing.Core;
using Attest.Testing.xUnit;
using Caliburn.Micro;
using LogoFX.Client.Testing.Contracts;
using Samples.Universal.Client.Presentation.Shell.ViewModels;
using Samples.Universal.Client.Tests.Integration.Infra.Shared;

namespace Samples.Universal.Client.Tests.Integration.Infra
{
    public abstract class IntegrationTestsBaseWithActivation :
        IntegrationTestsBase<ShellViewModel, TestBootstrapper>
    {               
        protected override ShellViewModel CreateRootObjectOverride(ShellViewModel rootObject)
        {
            ScreenExtensions.TryActivate(rootObject);
            return rootObject;
        }

        protected override void SetupOverride()
        {            
            LogoFX.Client.Testing.Shared.TestHelper.Setup();           
            ServiceRegistrationHelper.RegisterIntegrationObjects();
            ScenarioHelper.Add(new BuilderRegistrationService(), typeof(IBuilderRegistrationService));
        }

        protected override void OnBeforeTeardown()
        {
            base.OnBeforeTeardown();
            TestHelper.BeforeTeardown();
        }

        protected override void OnAfterTeardown()
        {
            base.OnAfterTeardown();
            TestHelper.AfterTeardown();
        }
    }
}