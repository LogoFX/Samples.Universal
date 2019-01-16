using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LogoFX.Core;
using Samples.Client.Data.Contracts.Providers;
using Samples.Client.Model.Contracts;
using Samples.Client.Model.Mappers;

namespace Samples.Client.Model
{
    [UsedImplicitly]
    class DataService : IDataService
    {
        private readonly IWarehouseProvider _warehouseProvider;

        public DataService(IWarehouseProvider warehouseProvider)
        {
            _warehouseProvider = warehouseProvider;
        }

        private readonly RangeObservableCollection<IWarehouseItem> _warehouseItems =
            new RangeObservableCollection<IWarehouseItem>();
        IEnumerable<IWarehouseItem> IDataService.WarehouseItems => _warehouseItems;

        async Task<IWarehouseItem> IDataService.NewWarehouseItemAsync() =>
            await ServiceRunner.RunWithResultAsync<IWarehouseItem>(
                () =>
                    new WarehouseItem("New Kind", 0d, 1)
                    {
                        IsNew = true
                    });

        private void GetWarehouseItems()
        {
            var warehouseItems = _warehouseProvider.GetWarehouseItems().Select(WarehouseMapper.MapToWarehouseItem);
            _warehouseItems.Clear();
            _warehouseItems.AddRange(warehouseItems);
        }

        private bool DeleteWarehouseItem(IWarehouseItem item)
        {
            var retVal = _warehouseProvider.DeleteWarehouseItem(item.Id);
            return retVal;
        }

        private void SaveWarehouseItem(IWarehouseItem item)
        {
            var dto = WarehouseMapper.MapToWarehouseDto(item);
            if (item.IsNew)
            {
                _warehouseProvider.CreateWarehouseItem(dto);
            }
            else
            {
                _warehouseProvider.UpdateWarehouseItem(dto);
            }
        }

        Task IDataService.GetWarehouseItemsAsync()
        {
            return ServiceRunner.RunAsync(() => GetWarehouseItems());
        }

        Task<bool> IDataService.DeleteWarehouseItemAsync(IWarehouseItem item)
        {
            return ServiceRunner.RunWithResultAsync(() => DeleteWarehouseItem(item));
        }

        Task IDataService.SaveWarehouseItemAsync(IWarehouseItem item)
        {
            return ServiceRunner.RunAsync(() => SaveWarehouseItem(item));
        }
    }
}
