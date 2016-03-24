using System;
using System.Collections.Generic;
using Attest.Fake.Moq;
using LogoFX.Client.Data.Fake.ProviderBuilders;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Providers;

namespace LogoFX.Samples.Specifications.Client.Data.Fake.ProviderBuilders
{
    [Serializable]
    public class LoginProviderBuilder : FakeBuilderBase<ILoginProvider>
    {        
        private readonly List<Tuple<string, string>> _users = new List<Tuple<string, string>>();
        private readonly Dictionary<string, bool> _isLoginAttemptSuccessfulCollection = new Dictionary<string, bool>();
        
        private LoginProviderBuilder()
        {

        }

        public static LoginProviderBuilder CreateBuilder()
        {
            return new LoginProviderBuilder();
        }

        public void WithUser(string username, string password)
        {
            _users.Add(new Tuple<string, string>(username, password));            
        }

        protected override void SetupFake()
        {            
            var initialSetup = CreateInitialSetup();

            var setup = initialSetup
               .AddMethodCallAsync<string, string>(t => t.Login(It.IsAny<string>(), It.IsAny<string>()),
                    (r, login, password) =>
                           _isLoginAttemptSuccessfulCollection.ContainsKey(login)
                               ? _isLoginAttemptSuccessfulCollection[login]
                                   ? r.Complete()
                                   : r.Throw(new Exception("unable to login"))
                               : r.Throw(new Exception("unable to login")));           

            setup.Build();
        }

        public void WithSuccessfulLogin(string username)
        {
            _isLoginAttemptSuccessfulCollection[username] = true;
        }
    }
}