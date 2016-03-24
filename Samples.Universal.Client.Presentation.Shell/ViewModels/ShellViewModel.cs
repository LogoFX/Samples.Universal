using Caliburn.Micro;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public class ShellViewModel : Screen
    {       
        private string _myMessage;
        public string MyMessage
        {
            get { return _myMessage; }
            set
            {
                _myMessage = value;
                NotifyOfPropertyChange(() => MyMessage);
            }
        }

        public void SayHello()
        {
            MyMessage = "Hello World!";
        }
    }
}
