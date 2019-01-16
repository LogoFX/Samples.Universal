using System.ComponentModel;
using JetBrains.Annotations;
using LogoFX.Client.Mvvm.ViewModel;
using LogoFX.Core;
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

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyOfPropertyChange(e.PropertyName);
        }

        public string Kind
        {
            get => Model.Kind;
            set => Model.Kind = value;
        }

        public int Quantity
        {
            get => Model.Quantity;
            set => Model.Quantity = value;
        }

        public double Price
        {
            get => Model.Price;
            set => Model.Price = value;
        }

        public double TotalCost => Model.TotalCost;
    }
}