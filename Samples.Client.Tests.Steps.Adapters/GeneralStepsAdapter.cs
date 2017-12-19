using TechTalk.SpecFlow;

namespace Samples.Client.Tests.Steps.Adapters
{
    [Binding]
    public class GeneralStepsAdapter
    {
        public GeneralSteps GeneralSteps { get; set; }

        public GeneralStepsAdapter(GeneralSteps generalSteps)
        {
            GeneralSteps = generalSteps;
        }

        [When(@"I open the application")]
        public void WhenIOpenTheApplication()
        {
            GeneralSteps.WhenIOpenTheApplication();
        }
    }
}