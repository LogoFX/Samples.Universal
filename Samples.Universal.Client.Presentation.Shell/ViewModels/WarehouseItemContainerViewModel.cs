using System.Threading.Tasks;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public sealed class WarehouseItemContainerViewModel : EditableObjectViewModel<IWarehouseItem>
    {
        private readonly IWarehouseItem _model;
        private readonly IDataService _dataService;
        private readonly IMainViewModel _mainViewModel;

        public WarehouseItemContainerViewModel(
            IWarehouseItem model,
            IDataService dataService,
            IMainViewModel mainViewModel) :
            base(model)
        {
            _model = model;
            _dataService = dataService;
            _mainViewModel = mainViewModel;
        }

        public string CommandsContext => WarehouseItem.Model.IsNew ? "New" : "Existing";

        private WarehouseItemCommandsViewModel _commands;
        public WarehouseItemCommandsViewModel Commands => _commands ??
                                                          (_commands = new WarehouseItemCommandsViewModel(_mainViewModel, ApplyCommand));

        private WarehouseItemViewModel _item;
        public WarehouseItemViewModel WarehouseItem => _item ??
                                              (_item = new WarehouseItemViewModel(_model));

        protected override async Task<bool> SaveMethod(IWarehouseItem model)
        {
            await _dataService.SaveWarehouseItemAsync(Model);
            return true;
        }
    }
}