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

        public Task GetWarehouseItemsAsync()
        {
            return ServiceRunner.RunAsync(() => GetWarehouseItemsInternal());
        }

        private void GetWarehouseItemsInternal()
        {
            var warehouseItems = _warehouseProvider.GetWarehouseItems().Select(WarehouseMapper.MapToWarehouseItem);
            _warehouseItems.Clear();
            _warehouseItems.AddRange(warehouseItems);
        }

        Task IDataService.DeleteWarehouseItemAsync(IWarehouseItem item) => ServiceRunner.RunAsync(() =>
        {
            var retVal = _warehouseProvider.DeleteWarehouseItem(item.Id);

            if (retVal)
            {
                _warehouseItems.Remove(item);
            }
        });

        async Task IDataService.SaveWarehouseItemAsync(IWarehouseItem item)
        {
            var dto = WarehouseMapper.MapToWarehouseDto(item);
            if (item.IsNew)
            {
                await ServiceRunner.RunAsync(() => _warehouseProvider.CreateWarehouseItem(dto));
            }
            else
            {
                await ServiceRunner.RunAsync(() => _warehouseProvider.UpdateWarehouseItem(dto));
            }
        }
    }
}
