using BoDi;
using Samples.Client.Tests.Contracts;
using Samples.Client.Tests.EndToEnd.Infra.Launcher;
using Solid.Common;
using Solid.IoC.Adapters.ObjectContainer;
using Solid.Practices.IoC;
using TechTalk.SpecFlow;

namespace Samples.Client.Tests.Specs
{
    [Binding]
    sealed class LifecycleHook
    {
        private readonly IIocContainer _iocContainer;

        public LifecycleHook(ObjectContainer objectContainer)
        {
            _iocContainer = new ObjectContainerAdapter(objectContainer);
        }

        [BeforeTestRun]
        public static void BeforeAllScenarios()
        {
            PlatformProvider.Current = new NetStandardPlatformProvider();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _iocContainer.Initialize();
            var setupService = _iocContainer.Resolve<ISetupService>();
            setupService.Setup();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _iocContainer.Teardown();
        }
    }
}
