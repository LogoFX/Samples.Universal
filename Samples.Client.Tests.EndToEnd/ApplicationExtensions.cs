using System;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace Samples.Client.Tests.EndToEnd
{
    public static class ApplicationExtensions
    {
        public static Window GetMainWindowEx(this Application app)
        {            
            app.WaitWhileBusy();
            Func<Window> getWindow = () =>
            {
                using (var automation = new UIA3Automation())
                {                    
                    var mainWindow = app.GetMainWindow(automation);                    
                    if (mainWindow == null ||
                        mainWindow.IsOffscreen ||
                        mainWindow.IsEnabled == false)
                    {

                        throw new Exception();
                    }
                    return mainWindow;

                }
            };
            return getWindow.ExecuteWithResult(20, TimeSpan.FromMilliseconds(500));
        }

        public static Window GetWindowEx(this Application app, string title)
        {
            //if (app.HasExited)
            //{
            //    return null;
            //}
            app.WaitWhileBusy();
            Func<Window> getWindow = () =>
            {
                using (var automation = new UIA3Automation())
                {
                    var topWindows = app.GetAllTopLevelWindows(automation);
                    var childWindows = topWindows.SelectMany(t => t.ModalWindows);
                    var allWindows = topWindows.Concat(childWindows).ToArray();
                    var window = allWindows.SingleOrDefault(x => x.Title == title);                    
                    if (window == null ||
                        window.Properties.IsOffscreen ||
                        window.Properties.IsEnabled == false)
                    {

                        throw new Exception();
                    }
                    return window;

                }
            };
            return getWindow.ExecuteWithResult(5, TimeSpan.FromMilliseconds(500));
        }
    }
}
