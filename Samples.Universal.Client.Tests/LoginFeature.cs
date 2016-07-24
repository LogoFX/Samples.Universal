﻿using Samples.Client.Tests.Steps;
using Samples.Universal.Client.Tests.Integration.Infra;
using Xunit;

namespace Samples.Universal.Client.Tests
{    
    public class LoginFeature : IntegrationTestsBaseWithActivation
    {
        [Fact]
        public void LoginScreenIsDisplayedFirst()
        {
            var GeneralSteps = Resolver.Resolve<GeneralSteps>();
            GeneralSteps.WhenIOpenTheApplication();

            LoginSteps.ThenTheLoginScreenIsDisplayed();
        }

        [Fact]
        public void NavigateToTheMainScreenWheTheLoginIsSuccessful()
        {
            var userName = "Admin";           
            GivenLoginSteps.SetupAuthenticatedUserWithUsername(userName);
            GivenLoginSteps.SetupLoginSuccessfullyWithUsername(userName);

            var GeneralSteps = Resolver.Resolve<GeneralSteps>();
            GeneralSteps.WhenIOpenTheApplication();
            LoginSteps.WhenISetTheUsernameTo(userName);
            LoginSteps.WhenILogInToTheSystem();

            MainSteps.ThenApplicationNavigatesToTheMainScreen();
        }
    }
}
