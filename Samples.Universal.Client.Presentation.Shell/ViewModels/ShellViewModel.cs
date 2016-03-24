using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Services;
using LogoFX.Core;
using Samples.Universal.Client.Model.Contracts;
using Samples.Universal.Client.Model.Shared;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class ShellViewModel : Conductor<INotifyPropertyChanged>.Collection.OneActive     
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
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        public bool IsLoggedIn
        {
            get { return UserContext.Current != null; }
        }        

        private LoginViewModel _loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get { return _loginViewModel ?? (_loginViewModel = CreateLoginViewModel()); }
        }

        private LoginViewModel CreateLoginViewModel()
        {
            return new LoginViewModel(_loginService);
        }

        private MainViewModel _mainViewModel;
        public MainViewModel MainViewModel
        {
            get { return _mainViewModel ?? (_mainViewModel = CreateMainViewModel()); }
        }

        private MainViewModel CreateMainViewModel()
        {
            return new MainViewModel(_dataService);
        }

        public override string DisplayName
        {
            get { return string.Empty; }
            set { }
        }

        protected override async void OnViewLoaded(object view)
        {
            ActivateItem(LoginViewModel);            
        }       

        private void OnLoggedInSuccessfully(object sender, EventArgs eventArgs)
        {
            ActivateItem(MainViewModel);
        }
    }
}
