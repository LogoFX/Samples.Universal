namespace Samples.Client.Tests.Contracts
{
    public interface IStartClientApplicationService
    {
        void StartApplication();
    }

    public interface ISetupService
    {
        void Setup();
    }

    public interface ITeardownService
    {
        void Teardown();
    }
}
