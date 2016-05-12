#if FAKE
using Attest.Testing.Core;
using LogoFX.Client.Testing.Contracts;
using Samples.Client.Data.Fake.ProviderBuilders;

#endif

#if REAL
#endif

namespace Samples.Client.Tests.Steps
{
    public static class GivenLoginSteps
    {
        public static void SetupAuthenticatedUserWithUsername(string username)
        {
#if FAKE
            var loginProviderBuilder = ScenarioHelper.GetOrCreate(LoginProviderBuilder.CreateBuilder);
            loginProviderBuilder.WithUser(username, string.Empty);
            ScenarioHelper.Get<IBuilderRegistrationService>().RegisterBuilder(loginProviderBuilder);
#endif

#if REAL
            //put here real Setup
#endif
        }        

        public static void SetupLoginSuccessfullyWithUsername(string username)
        {
#if FAKE
            var loginProviderBuilder = ScenarioHelper.GetOrCreate(LoginProviderBuilder.CreateBuilder);
            loginProviderBuilder.WithSuccessfulLogin(username);
            ScenarioHelper.Get<IBuilderRegistrationService>().RegisterBuilder(loginProviderBuilder);
#endif

#if REAL
            //put here real Setup
#endif
        }
    }
}