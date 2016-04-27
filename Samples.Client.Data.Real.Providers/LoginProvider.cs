using System.Threading.Tasks;
using Samples.Client.Data.Contracts.Providers;

namespace Samples.Client.Data.Real.Providers
{
    class LoginProvider : ILoginProvider
    {
        public Task Login(string username, string password)
        {
            return Task.Delay(200);
        }
    }
}
