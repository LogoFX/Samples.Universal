using JetBrains.Annotations;
using Samples.Client.Model.Contracts;

namespace Samples.Client.Model
{
    [UsedImplicitly]
    class WarehouseItem : AppModel, IWarehouseItem
    {
        public WarehouseItem(
            int id,
            string kind, 
            double price, 
            int quantity)
        {
            Id = id;
            Kind = kind;
            Price = price;
            Quantity = quantity;            
        }

        private string _kind;
        public string Kind
        {
            get => _kind;
            set => SetProperty(ref _kind, value);
        }

        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                SetProperty(ref _price, value);
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                SetProperty(ref _quantity, value);
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        public double TotalCost => _quantity*_price;
    }
}
