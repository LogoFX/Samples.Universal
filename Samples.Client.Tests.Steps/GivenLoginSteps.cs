#if FAKE
using Attest.Testing.Contracts;
using Samples.Client.Data.Fake.ProviderBuilders;
#endif

#if REAL
#endif

namespace Samples.Client.Tests.Steps
{
    public class GivenLoginSteps
    {
        private readonly IBuilderRegistrationService _builderRegistrationService;
        private readonly LoginProviderBuilder _loginProviderBuilder;

        public GivenLoginSteps(
            IBuilderRegistrationService builderRegistrationService, 
            LoginProviderBuilder loginProviderBuilder)
        {
            _builderRegistrationService = builderRegistrationService;
            _loginProviderBuilder = loginProviderBuilder;
        }

        public void SetupAuthenticatedUserWithUsername(string username)
        {
#if FAKE            
            _loginProviderBuilder.WithUser(username, string.Empty);
            _builderRegistrationService.RegisterBuilder(_loginProviderBuilder);
#endif

#if REAL
            //put here real Setup
#endif
        }        

        public void SetupLoginSuccessfullyWithUsername(string username)
        {
#if FAKE
            _loginProviderBuilder.WithSuccessfulLogin(username);
            _builderRegistrationService.RegisterBuilder(_loginProviderBuilder);
#endif

#if REAL
            //put here real Setup
#endif
        }
    }
}