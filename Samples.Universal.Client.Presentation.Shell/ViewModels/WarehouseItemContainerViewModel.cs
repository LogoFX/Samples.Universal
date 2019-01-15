using LogoFX.Client.Mvvm.ViewModel;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public sealed class WarehouseItemContainerViewModel : ObjectViewModel<IWarehouseItem>
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

        public string CommandsContext => Item.Model.IsNew ? "New" : "Existing";

        private WarehouseItemCommandsViewModel _commands;
        public WarehouseItemCommandsViewModel Commands => _commands ??
                                                          (_commands = new WarehouseItemCommandsViewModel(_mainViewModel));

        private WarehouseItemViewModel _item;
        public WarehouseItemViewModel Item => _item ??
                                              (_item = new WarehouseItemViewModel(_model));
    }
}