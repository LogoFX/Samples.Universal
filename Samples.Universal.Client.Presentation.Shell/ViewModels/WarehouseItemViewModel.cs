using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using Samples.Client.Model.Contracts;

namespace Samples.Universal.Client.Presentation.Shell.ViewModels
{
    [UsedImplicitly]
    public class WarehouseItemViewModel : ObjectViewModel<IWarehouseItem>
    {
        private readonly IDataService _dataService;

        public WarehouseItemViewModel(
            IWarehouseItem model,
            IDataService dataService) 
            : base(model)
        {
            _dataService = dataService;
        }

        public string Kind => Model.Kind;

        public int Quantity => Model.Quantity;

        public double Price => Model.Price;

        public double TotalCost => Model.TotalCost;
    }
}