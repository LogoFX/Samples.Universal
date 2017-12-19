namespace Samples.Client.Tests.Contracts.ScreenObjects
{
    public interface ILoginScreenObject
    {
        bool IsActive();
        void SetUsername(string username);
        void Login();
    }
}
