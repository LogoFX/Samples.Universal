using System.Threading.Tasks;

namespace Samples.Universal.Client.Data.Contracts.Providers
{
    public interface ILoginProvider
    {
        Task Login(string username, string password);
    }
}
