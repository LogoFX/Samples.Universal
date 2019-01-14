using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.Security.Credentials;
using Windows.Storage;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public sealed class LoginViewModel : BusyScreen
    {
        private static readonly string SavePasswordKey = "SavePassword";

        private static readonly string CredentialResourceNameKey = "Samples.Universal.Credentials";

        private readonly ILoginService _loginService;

        public LoginViewModel(
            ILoginService loginService)
        {
            _loginService = loginService;
            DisplayName = "Login";
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(SavePasswordKey, out var savePassword))
            {
                SavePassword = (bool)savePassword;
            }

            var loginCredential = GetCredentialFromLocker();

            if (loginCredential != null)
            {
                // There is a credential stored in the locker.
                // Populate the Password property of the credential
                // for automatic login.
                loginCredential.RetrievePassword();
                UserName = loginCredential.UserName;
                Password = loginCredential.Password;
            }
        }

        private PasswordCredential GetCredentialFromLocker()
        {
            PasswordCredential credential = null;

            var vault = new PasswordVault();
            IReadOnlyList<PasswordCredential> credentialList;
            try
            {
                credentialList = vault.FindAllByResource(CredentialResourceNameKey);
            }
            catch (Exception)
            {
                return null;
            }

            if (credentialList.Count > 0)
            {
                if (credentialList.Count == 1)
                {
                    credential = credentialList[0];
                }
                else
                {
                    credential = vault.Retrieve(CredentialResourceNameKey, UserName);
                }
            }

            return credential;
        }

        public event EventHandler LoggedInSuccessfully;

        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = ActionCommand
                           .When(() => !string.IsNullOrWhiteSpace(UserName))
                           .Do(async () =>
                           {
                               LoginFailureCause = null;

                               try
                               {
                                   IsBusy = true;
                                   await _loginService.LoginAsync(UserName, Password);
                                   OnLoginSuccess();
                               }

                               catch (Exception ex)
                               {
                                   LoginFailureCause = $"Failed to log in: {ex.Message}";
                               }

                               finally
                               {
                                   Password = string.Empty;
                                   IsBusy = false;
                               }
                           })
                           .RequeryOnPropertyChanged(this, () => Password));
            }
        }

        private ICommand _cancelCommand;
        public ICommand LoginCancelCommand
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = ActionCommand
                           .Do(() =>
                           {
                               TryClose();
                           }));
            }
        }

        private bool _savePassword;

        //TODO: make 'Save Password' workable
        public bool SavePassword
        {
            get => _savePassword;
            set => Set(ref _savePassword, value);
        }

        private string _loginFailureCause;
        public string LoginFailureCause
        {
            get => _loginFailureCause;
            set
            {
                if (_loginFailureCause == value)
                {
                    return;
                }

                _loginFailureCause = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => IsLoginFailureTextVisible);
            }
        }

        public bool IsLoginFailureTextVisible => string.IsNullOrWhiteSpace(LoginFailureCause) == false;

        private bool _isUserAuthenticated;
        public bool IsUserAuthenticated
        {
            get => _isUserAuthenticated;
            private set
            {
                if (_isUserAuthenticated == value)
                    return;

                _isUserAuthenticated = value;
                NotifyOfPropertyChange();
            }
        }

        public string CurrentDomain
        {
            get;
            private set;
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyOfPropertyChange();
            }
        }

        private string _password = string.Empty;

        public string Password
        {
            get => _password;
            set
            {
                if (_password == value)
                    return;

                _password = value;
                NotifyOfPropertyChange();
            }
        }

        private void OnLoginSuccess()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[SavePasswordKey] = SavePassword;

            var vault = new PasswordVault();
            var passwordCredential = new PasswordCredential(CredentialResourceNameKey, UserName, Password);
            if (SavePassword)
            {
                vault.Add(passwordCredential);
            }
            else
            {
                vault.Remove(passwordCredential);
            }

            TryClose(true);
            LoggedInSuccessfully?.Invoke(this, new EventArgs());
        }
    }
}