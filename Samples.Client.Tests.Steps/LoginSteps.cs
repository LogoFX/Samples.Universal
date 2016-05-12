using Attest.Testing.Core;
using Samples.Client.Tests.Contracts.ScreenObjects;
using Shouldly;

namespace Samples.Client.Tests.Steps
{
    public static class LoginSteps
    {
        private static readonly ILoginScreenObject _loginScreenObject = ScenarioHelper.Get<ILoginScreenObject>();        

        public static void WhenISetTheUsernameTo(string username)
        {
            _loginScreenObject.SetUsername(username);
        }

        public static void WhenILogInToTheSystem()
        {
            _loginScreenObject.Login();
        }

        public static void ThenTheLoginScreenIsDisplayed()
        {
            var isActive = _loginScreenObject.IsActive();
            isActive.ShouldBeTrue();
        }
    }
}
