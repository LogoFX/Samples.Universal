using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.WinRTContainer;
using Samples.Universal.Client.Presentation.Shell.ViewModels;

namespace Samples.Universal.Client.Presentation.Shell
{
    public class AppBootstrapper : BootstrapperContainerBase<WinRTContainerAdapter, WinRTContainer>.WithRootObject<ShellViewModel>
    {
        private static readonly WinRTContainer _iocContainer = new WinRTContainer();

        public AppBootstrapper() : base(_iocContainer, c => new WinRTContainerAdapter(c))
        {
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _iocContainer.RegisterNavigationService(rootFrame);
        }        
    }    
}