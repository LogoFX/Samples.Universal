using System.Collections.Generic;
using System.Threading.Tasks;
//using Attest.Fake.Builders;
//using JetBrains.Annotations;
using Samples.Universal.Client.Data.Contracts.Dto;
using Samples.Universal.Client.Data.Contracts.Providers;
//using Samples.Universal.Client.Data.Fake.Containers;
//using Samples.Universal.Client.Data.Fake.ProviderBuilders;
//using Solid.Practices.Scheduling;

namespace Samples.Universal.Client.Data.Fake.Providers
{
    //[UsedImplicitly]
    //class FakeWarehouseProvider : FakeProviderBase<WarehouseProviderBuilder, IWarehouseProvider>, IWarehouseProvider
    //{
    //    private readonly WarehouseProviderBuilder _warehouseProviderBuilder;
    //    private readonly Random _random = new Random();

    //    public FakeWarehouseProvider(
    //        WarehouseProviderBuilder warehouseProviderBuilder,
    //        IWarehouseContainer warehouseContainer)
    //    {
    //        _warehouseProviderBuilder = warehouseProviderBuilder;
    //        _warehouseProviderBuilder.WithWarehouseItems(warehouseContainer.WarehouseItems);
    //    }

    //    async Task<IEnumerable<WarehouseItemDto>> IWarehouseProvider.GetWarehouseItems()
    //    {
    //        await TaskRunner.RunAsync(() => Thread.Sleep(_random.Next(2000)));
    //        var service = GetService(() => _warehouseProviderBuilder, b => b);
    //        var warehouseItems = await service.GetWarehouseItems();
    //        return warehouseItems;
    //    }
    //}

    class FakeWarehouseProvider : IWarehouseProvider
    {
        public Task<IEnumerable<WarehouseItemDto>> GetWarehouseItems()
        {
            return Task.FromResult((IEnumerable<WarehouseItemDto>) new WarehouseItemDto [] {});
        }
    }
}
