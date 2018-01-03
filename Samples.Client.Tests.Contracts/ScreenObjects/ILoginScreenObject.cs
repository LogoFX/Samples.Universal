namespace Samples.Client.Tests.Contracts.ScreenObjects
{
    public interface ILoginScreenObject
    {
        bool IsActive();
        void SetUsername(string username);
        void SetPassword(string password);
        void Login();
    }
}
