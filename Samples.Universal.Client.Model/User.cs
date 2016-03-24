using Samples.Universal.Client.Model.Contracts;

namespace Samples.Universal.Client.Model
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