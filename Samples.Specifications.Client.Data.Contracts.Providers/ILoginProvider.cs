using System.Threading.Tasks;

namespace LogoFX.Samples.Specifications.Client.Data.Contracts.Providers
{
    public interface ILoginProvider
    {
        Task Login(string username, string password);
    }
}
