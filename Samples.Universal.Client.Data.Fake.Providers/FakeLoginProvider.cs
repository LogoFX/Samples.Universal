using System.Threading.Tasks;
using Attest.Fake.Builders;
using JetBrains.Annotations;
using Samples.Universal.Client.Data.Contracts.Providers;
using Samples.Universal.Client.Data.Fake.Containers;
using Samples.Universal.Client.Data.Fake.ProviderBuilders;

namespace Samples.Universal.Client.Data.Fake.Providers
{
    [UsedImplicitly]
    class FakeLoginProvider : FakeProviderBase<LoginProviderBuilder, ILoginProvider>, ILoginProvider
    {
        private readonly LoginProviderBuilder _loginProviderBuilder;

        public FakeLoginProvider(
            LoginProviderBuilder loginProviderBuilder,
            IUserContainer userContainer)
        {
            _loginProviderBuilder = loginProviderBuilder;
            foreach (var user in userContainer.Users)
            {
                _loginProviderBuilder.WithUser(user.Item1, user.Item2);
                _loginProviderBuilder.WithSuccessfulLogin(user.Item1);
            }
        }

        async Task ILoginProvider.Login(string username, string password)
        {
            var service = GetService(() => _loginProviderBuilder, b => b);
            await service.Login(username, password);
        }
    }

    //class FakeLoginProvider : ILoginProvider
    //{
    //    public Task Login(string username, string password)
    //    {
    //        return Task.Run(() => { });
    //    }
    //}
}