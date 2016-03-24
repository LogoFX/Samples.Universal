using LogoFX.Samples.Specifications.Client.Model.Contracts;

namespace LogoFX.Samples.Specifications.Client.Model
{
    class User : AppModel, IUser
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; }
    }
}