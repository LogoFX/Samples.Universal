using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using LogoFX.Client.Bootstrapping;
using LogoFX.Client.Bootstrapping.Adapters.SimpleContainer;
using Samples.Universal.Client.Presentation.Shell.ViewModels;
using Solid.Practices.Composition;

namespace Samples.Universal.Launcher
{
    public class AppBootstrapper : BootstrapperContainerBase<ExtendedSimpleContainerAdapter>.WithRootObject<ShellViewModel>
    {
        private static readonly ExtendedSimpleContainerAdapter _iocContainer = new ExtendedSimpleContainerAdapter();

        public AppBootstrapper() : base(_iocContainer)
        {
        }

        public override CompositionOptions CompositionOptions => new CompositionOptions
        {
            Prefixes = new [] { "Samples.Universal" }
        };

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            RegisterNavigationService(rootFrame);
        }

        private void RegisterNavigationService(Frame rootFrame, bool treatViewAsLoaded = false, bool cacheViewModels = false)
        {
            INavigationService navigationService = cacheViewModels ? new CachingFrameAdapter(rootFrame, treatViewAsLoaded) : new FrameAdapter(rootFrame, treatViewAsLoaded);
            _iocContainer.RegisterInstance(navigationService);
        }
    }
}
