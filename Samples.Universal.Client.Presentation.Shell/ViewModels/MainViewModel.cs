using System.Threading.Tasks;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Contracts;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class MainViewModel : Screen, ICanBeBusy
    {
        private readonly IDataService _dataService;

        public MainViewModel(
            IDataService dataService)
        {
            _dataService = dataService;

            NewWarehouseItem();
        }

        internal async void NewWarehouseItem()
        {
            IsBusy = true;

            try
            {
                var warehouseItem = await _dataService.NewWarehouseItemAsync();
                var newItem = new WarehouseItemContainerViewModel(warehouseItem, _dataService, this);
                ActiveWarehouseItem = newItem;
            }

            finally
            {
                IsBusy = false;
            }
        }


        public override string DisplayName
        {
            get => "Main";
            set { }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private WarehouseItemsViewModel _warehouseItems;

        public WarehouseItemsViewModel WarehouseItems =>
            _warehouseItems ?? (_warehouseItems = new WarehouseItemsViewModel(_dataService));

        private WarehouseItemContainerViewModel _activeWarehouseItem;
        public WarehouseItemContainerViewModel ActiveWarehouseItem
        {
            get => _activeWarehouseItem;
            set
            {
                if (_activeWarehouseItem == value)
                {
                    return;
                }

                if (_activeWarehouseItem != null)
                {
                    //_activeWarehouseItem.Saving -= OnSaving;
                    //_activeWarehouseItem.Saved -= OnSaved;
                }

                _activeWarehouseItem = value;

                if (_activeWarehouseItem != null)
                {
                    //_activeWarehouseItem.Saving += OnSaving;
                    //_activeWarehouseItem.Saved += OnSaved;
                }

                NotifyOfPropertyChange();
            }
        }

        internal async void DeleteSelectedItem()
        {
            IsBusy = true;

            try
            {
                await _dataService.DeleteWarehouseItemAsync(ActiveWarehouseItem?.Item.Model);
                await _dataService.GetWarehouseItemsAsync();
            }

            finally
            {
                IsBusy = false;
            }

            NewWarehouseItem();
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            await _dataService.GetWarehouseItemsAsync();
        }
    }
}