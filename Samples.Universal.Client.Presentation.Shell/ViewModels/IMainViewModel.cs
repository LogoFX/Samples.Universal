using System.ComponentModel;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        void NewWarehouseItem();

        void DeleteSelectedItem();

        WarehouseItemContainerViewModel ActiveWarehouseItem { get; }

        void WarehouseItemsSelectionChanged(WarehouseItemViewModel selectedItem);
    }
}