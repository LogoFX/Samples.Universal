﻿using TechTalk.SpecFlow;

namespace Samples.Client.Tests.Steps.Adapters
{
    [Binding]
    class GivenLoginStepsAdapter
    {
        public GivenLoginSteps GivenLoginSteps { get; set; }

        public GivenLoginStepsAdapter(GivenLoginSteps givenLoginSteps)
        {
            GivenLoginSteps = givenLoginSteps;
        }

        [Given(@"I am able to log in successfully with username ""(.*)""")]
        public void GivenIAmAbleToLogInSuccessfullyWithUsername(string userName)
        {
            GivenLoginSteps.SetupAuthenticatedUserWithUsername(userName);
            GivenLoginSteps.SetupLoginSuccessfullyWithUsername(userName);
        }
    }
}