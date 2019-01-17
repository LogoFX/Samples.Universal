using System.Collections;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.Commanding;
using LogoFX.Client.Mvvm.ViewModel;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class WarehouseItemsViewModel : PropertyChangedBase
    {
        private readonly IDataService _dataService;
        private readonly IMainViewModel _mainViewModel;

        public WarehouseItemsViewModel(
            IDataService dataService,
            IMainViewModel mainViewModel)
        {
            _dataService = dataService;
            _mainViewModel = mainViewModel;
        }

        private ICommand _selectionChangedCommand;

        public ICommand SelectionChangedCommand
        {
            get
            {
                return _selectionChangedCommand ??
                       (_selectionChangedCommand = ActionCommand<SelectionChangedEventArgs>
                           .When(e => true)
                           .Do(e =>
                           {
                               _mainViewModel.WarehouseItemsSelectionChanged(e.AddedItems.OfType<WarehouseItemViewModel>().SingleOrDefault());
                           }));
            }
        }

        private IEnumerable _warehouseItems;
        public IEnumerable WarehouseItems => _warehouseItems ?? (_warehouseItems = CreateWarehouseItems());

        private IEnumerable CreateWarehouseItems()
        {
            var wc = new WrappingCollection
            {
                FactoryMethod = o => new WarehouseItemViewModel((IWarehouseItem)o)
            }.WithSource(_dataService.WarehouseItems);

            return wc.AsView();
        }

        //***********************************
        // Workaround due to DataGrid not properly uses ListCollectionView.VectorChanged event.
        // In the event handler the DataGrid request just deleted item.
        //***********************************

        //TODO: remove this Workaround, when ListCollectionView (package LogoFX.Client.Mvvm.ViewModel) will fix.
        internal void BeginUpdate()
        {
            _warehouseItems = Enumerable.Empty<WarehouseItemViewModel>();
            NotifyOfPropertyChange(() => WarehouseItems);
        }

        //TODO: remove this Workaround, when ListCollectionView (package LogoFX.Client.Mvvm.ViewModel) will fix.
        internal void EndUpdate()
        {
            _warehouseItems = CreateWarehouseItems();
            NotifyOfPropertyChange(() => WarehouseItems);
        }
    }
}