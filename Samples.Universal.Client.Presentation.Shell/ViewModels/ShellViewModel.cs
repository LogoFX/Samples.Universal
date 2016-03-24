using System.Linq;
using Caliburn.Micro;
using Samples.Universal.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IDataService _dataService;

        public ShellViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

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

        public async void SayHello()
        {
            MyMessage = "Hello World!";
            await _dataService.GetWarehouseItemsAsync();
            MyMessage = _dataService.WarehouseItems.First().Kind;
        }
    }
}
