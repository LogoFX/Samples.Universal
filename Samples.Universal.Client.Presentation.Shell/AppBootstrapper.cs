using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.WinRTContainer;
using Samples.Universal.Client.Presentation.Shell.ViewModels;

namespace Samples.Universal.Client.Presentation.Shell
{
    public class AppBootstrapper : BootstrapperContainerBase<ShellViewModel, WinRTContainerAdapter>
    {
        private static readonly WinRTContainerAdapter _iocContainerAdapter = new WinRTContainerAdapter(new WinRTContainer());

        public AppBootstrapper() : base(_iocContainerAdapter)
        {
        }

        protected override void BeforeOnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif  
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _iocContainerAdapter.RegisterNavigationService(rootFrame);
        }

        public override string[] Prefixes => new[] {"Samples"};
    }    
}