using System.Threading.Tasks;
using JetBrains.Annotations;
using Samples.Client.Data.Contracts.Providers;
using Samples.Client.Model.Contracts;
using Samples.Client.Model.Shared;

namespace Samples.Client.Model
{
    [UsedImplicitly]
    class LoginService : ILoginService
    {
        private readonly ILoginProvider _loginProvider;

        public LoginService(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }

        public async Task LoginAsync(string username, string password)
        {
            await ServiceRunner.RunAsync(() => LoginInternal(username, password));
        }

        private void LoginInternal(string username, string password)
        {
            _loginProvider.Login(username, password);
            UserContext.Current = new User(username);
        }
    }
}