using Samples.Client.Model.Contracts;

namespace Samples.Client.Model
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