using System;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Core;
using Samples.Client.Model.Contracts;
using Samples.Client.Model.Shared;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        private readonly ILoginService _loginService;
        private readonly IDataService _dataService;

        public ShellViewModel(
            ILoginService loginService,
            IDataService dataService)
        {
            _loginService = loginService;
            _dataService = dataService;

            EventHandler strongHandler = OnLoggedInSuccessfully;
            LoginViewModel.LoggedInSuccessfully += WeakDelegate.From(strongHandler);
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public bool IsLoggedIn => UserContext.Current != null;

        private LoginViewModel _loginViewModel;
        public LoginViewModel LoginViewModel => _loginViewModel ?? (_loginViewModel = CreateLoginViewModel());

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_loginService);
        }

        private MainViewModel _mainViewModel;
        public MainViewModel MainViewModel => _mainViewModel ?? (_mainViewModel = CreateMainViewModel());

        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_dataService);
        }

        public override string DisplayName
        {
            get => string.Empty;
            set { }
        }

        protected override void OnViewLoaded(object view)
        {
            ActivateItem(LoginViewModel);
        }

        private void OnLoggedInSuccessfully(object sender, EventArgs eventArgs)
        {
            ActivateItem(MainViewModel);
        }
    }
}
