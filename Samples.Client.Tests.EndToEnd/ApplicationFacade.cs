using System.Linq;
using FlaUI.Core;
using LogoFX.Client.Testing.Contracts;

namespace Samples.Client.Tests.EndToEnd
{
    public class ApplicationFacade : IApplicationFacade
    {
        /// <summary>Starts the application.</summary>
        /// <param name="startupPath">The startup path.</param>
        public void Start(string startupPath)
        {
            var processes = System.Diagnostics.Process.GetProcesses();
            var appFrameHost = processes.FirstOrDefault(t => t.ProcessName.Contains("ApplicationFrameHost"));            
            Application.LaunchStoreApp(startupPath);
            ApplicationContext.Application =  Application.Attach(appFrameHost);
            ApplicationContext.Application.WaitWhileBusy();
        }

        public void Stop()
        {
            ApplicationContext.Application.Kill();
        }
    }
}