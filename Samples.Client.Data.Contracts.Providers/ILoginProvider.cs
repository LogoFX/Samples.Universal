using System.Threading.Tasks;

namespace Samples.Client.Data.Contracts.Providers
{
    public interface ILoginProvider
    {
        Task Login(string username, string password);
    }
}
