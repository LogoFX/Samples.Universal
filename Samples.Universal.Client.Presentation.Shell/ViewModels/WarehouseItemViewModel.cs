using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class WarehouseItemViewModel : ObjectViewModel<IWarehouseItem>
    {
        public WarehouseItemViewModel(IWarehouseItem model) : base(model)
        {

        }

        public string Kind => Model.Kind;

        public int Quantity => Model.Quantity;

        public double Price => Model.Price;

        public double TotalCost => Model.TotalCost;
    }
}