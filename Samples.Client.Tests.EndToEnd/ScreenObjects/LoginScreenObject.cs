using FlaUI.Core.AutomationElements;
using Samples.Client.Tests.Contracts.ScreenObjects;

namespace Samples.Client.Tests.EndToEnd.ScreenObjects
{
    class LoginScreenObject : ILoginScreenObject
    {
        public void Login()
        {
            var loginScreen = GetLoginScreen();
            var loginButton = loginScreen.FindFirstDescendant("Login_SignIn").AsButton();
            loginButton.Click();            
        }

        public void SetUsername(string username)
        {
            var loginScreen = GetLoginScreen();
            for (int i = 0; i < 3; i++)
            {
                var userNameTextBox = loginScreen.FindFirstDescendant("Login_UserName").AsTextBox();
                userNameTextBox.Enter(username);
                if (userNameTextBox.Text == username)
                {
                    break;
                }
            }            
        }

        public void SetPassword(string password)
        {
            var loginScreen = GetLoginScreen();
            var passwordBox = loginScreen.FindFirstDescendant("Login_Password").AsTextBox();
            passwordBox.Enter(password);
        }

        private Window GetLoginScreen()
        {
            var application = ApplicationContext.Application;
            var loginScreen = application.GetMainWindowEx();            
            return loginScreen;
        }

        public string GetErrorMessage()
        {
            var loginScreen = GetLoginScreen();
            var errorLabel = loginScreen.FindFirstDescendant("Login_FailureTextBlock").AsLabel();
            return errorLabel.Text;
        }

        public bool IsActive()
        {
            //TODO
            return true;
        }
    }
}
