using Samples.Client.Model.Contracts;

namespace Samples.Client.Model.Shared
{
    public static class UserContext
    {
        private static IUser _currentUser;

        public static IUser Current
        {
            get => _currentUser;
            set
            {
                if (_currentUser == value)
                {
                    return;
                }

                _currentUser = value;         
            }
        }        
    }
}
