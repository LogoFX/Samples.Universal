using Samples.Client.Tests.Contracts.ScreenObjects;

namespace Samples.Client.Tests.EndToEnd.ScreenObjects
{
    class MainScreenObject : IMainScreenObject
    {
        public bool IsActive()
        {
            var shell = ApplicationContext.Application.GetMainWindowEx();
            return shell.Title.Contains("Main");
        }
    }
}