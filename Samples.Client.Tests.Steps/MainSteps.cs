using Attest.Testing.Core;
using FluentAssertions;
using Samples.Client.Tests.Contracts.ScreenObjects;

namespace Samples.Client.Tests.Steps
{
    public static class MainSteps
    {
        private static readonly IMainScreenObject _mainScreenObject = ScenarioHelper.Get<IMainScreenObject>();

        public static void ThenApplicationNavigatesToTheMainScreen()
        {
            var isActive = _mainScreenObject.IsActive();
            isActive.Should().BeTrue();
        }
    }
}