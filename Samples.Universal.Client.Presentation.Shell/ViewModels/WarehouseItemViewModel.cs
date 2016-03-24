using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using Samples.Universal.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class WarehouseItemViewModel : ObjectViewModel<IWarehouseItem>
    {
        public WarehouseItemViewModel(IWarehouseItem model) : base(model)
        {
            
        }

        public string Kind
        {
            get { return Model.Kind; }
        }

        public int Quantity
        {
            get { return Model.Quantity; }
        }

        public double Price
        {
            get { return Model.Price; }
        }

        public double TotalCost
        {
            get { return Model.TotalCost; }
        }
    }
}
