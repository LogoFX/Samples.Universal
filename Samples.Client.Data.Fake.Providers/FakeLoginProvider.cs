using System.Threading.Tasks;
using Attest.Fake.Builders;
using JetBrains.Annotations;
using Samples.Client.Data.Contracts.Providers;
using Samples.Client.Data.Fake.Containers;
using Samples.Client.Data.Fake.ProviderBuilders;

namespace Samples.Client.Data.Fake.Providers
{
    [UsedImplicitly]
    class FakeLoginProvider : FakeProviderBase<LoginProviderBuilder, ILoginProvider>, ILoginProvider
    {
        public FakeLoginProvider(
            LoginProviderBuilder loginProviderBuilder,
            IUserContainer userContainer)
            : base(loginProviderBuilder)
        {
            foreach (var user in userContainer.Users)
            {
                loginProviderBuilder.WithUser(user.Item1, user.Item2);
                loginProviderBuilder.WithSuccessfulLogin(user.Item1);
            }
        }

        void ILoginProvider.Login(string username, string password) => GetService().Login(username, password);
    }
}