using TechTalk.SpecFlow;

namespace Samples.Client.Tests.Steps.Adapters
{
    [Binding]
    class MainStepsAdapter
    {
        public MainSteps MainSteps { get; set; }

        public MainStepsAdapter(MainSteps mainSteps)
        {
            MainSteps = mainSteps;
        }

        [Then(@"Application navigates to the main screen")]
        public void ThenApplicationNavigatesToTheMainScreen()
        {
            MainSteps.ThenApplicationNavigatesToTheMainScreen();
        }
    }
}
