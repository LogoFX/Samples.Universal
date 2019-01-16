using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class WarehouseItemViewModel : ObjectViewModel<IWarehouseItem>
    {
        public WarehouseItemViewModel(
            IWarehouseItem model) 
            : base(model)
        {
        }

        public string Kind
        {
            get => Model.Kind;
            set => Model.Kind = value;
        }

        public int Quantity
        {
            get => Model.Quantity;
            set
            {
                Model.Quantity = value;
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        public double Price
        {
            get => Model.Price;
            set
            {
                Model.Price = value;
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        public double TotalCost => Model.TotalCost;
    }
}