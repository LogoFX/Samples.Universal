using System;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel.Contracts;
using LogoFX.Client.Mvvm.ViewModel.Extensions;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class MainViewModel : Screen, ICanBeBusy, IMainViewModel
    {
        #region Fields

        private readonly IDataService _dataService;

        #endregion

        #region Constructors

        public MainViewModel(
            IDataService dataService)
        {
            _dataService = dataService;

            NewWarehouseItemInternal();
        }

        #endregion

        #region Public Properties

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set => Set(ref _isBusy, value);
        }

        private WarehouseItemsViewModel _warehouseItems;

        public WarehouseItemsViewModel WarehouseItems =>
            _warehouseItems ?? (_warehouseItems = new WarehouseItemsViewModel(_dataService, this));

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
                    _activeWarehouseItem.Saving -= OnSaving;
                }

                _activeWarehouseItem = value;

                if (_activeWarehouseItem != null)
                {
                    _activeWarehouseItem.Saving += OnSaving;
                }

                NotifyOfPropertyChange();
            }
        }

        private async void OnSaved(object sender, ResultEventArgs e)
        {
            ((WarehouseItemContainerViewModel)sender).Saved -= OnSaved;

            IsBusy = true;
            try
            {
                await _dataService.GetWarehouseItemsAsync();
                WarehouseItems.EndUpdate();

            }

            finally
            {
                IsBusy = false;
            }

            NewWarehouseItemInternal();
        }

        private void OnSaving(object sender, EventArgs e)
        {
            var itemContainerViewModel = ((WarehouseItemContainerViewModel)sender);
            if (itemContainerViewModel.Model.IsNew)
            {
                itemContainerViewModel.Saved += OnSaved;
                WarehouseItems.BeginUpdate();
                IsBusy = true;
            }
        }

        #endregion

        #region Private Members

        private async void NewWarehouseItemInternal()
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

        private async void DeleteSelectedItemInternal()
        {
            IsBusy = true;

            try
            {
                WarehouseItems.BeginUpdate();

                await _dataService.DeleteWarehouseItemAsync(ActiveWarehouseItem?.WarehouseItem.Model);
                await _dataService.GetWarehouseItemsAsync();

                WarehouseItems.EndUpdate();
            }

            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                throw;
            }

            finally
            {
                IsBusy = false;
            }

            NewWarehouseItemInternal();
        }

        private void WarehouseItemsSelectionChangedInternal(WarehouseItemViewModel selectedItem)
        {
            if (selectedItem == null)
            {
                ActiveWarehouseItem = null;
                return;
            }

            ActiveWarehouseItem = new WarehouseItemContainerViewModel(selectedItem.Model, _dataService, this);
        }

        #endregion

        #region Overrides

        public override string DisplayName
        {
            get => "Main";
            set { }
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            await _dataService.GetWarehouseItemsAsync();
        }

        #endregion

        #region ICanBeBusy

        bool ICanBeBusy.IsBusy
        {
            get => IsBusy;
            set => IsBusy = value;
        }

        #endregion

        #region IMainViewModel

        WarehouseItemContainerViewModel IMainViewModel.ActiveWarehouseItem => ActiveWarehouseItem;

        void IMainViewModel.NewWarehouseItem()
        {
            NewWarehouseItemInternal();
        }

        void IMainViewModel.DeleteSelectedItem()
        {
            DeleteSelectedItemInternal();
        }

        void IMainViewModel.WarehouseItemsSelectionChanged(WarehouseItemViewModel selectedItem)
        {
            WarehouseItemsSelectionChangedInternal(selectedItem);
        }

        #endregion
    }
}