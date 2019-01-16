using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Attest.Fake.Builders;
using JetBrains.Annotations;
using Samples.Client.Data.Contracts.Dto;
using Samples.Client.Data.Contracts.Providers;
using Samples.Client.Data.Fake.Containers;
using Samples.Client.Data.Fake.ProviderBuilders;

namespace Samples.Client.Data.Fake.Providers
{
    [UsedImplicitly]
    internal sealed class FakeWarehouseProvider : FakeProviderBase<WarehouseProviderBuilder, IWarehouseProvider>,
        IWarehouseProvider
    {
        private readonly Random _random = new Random();

        public FakeWarehouseProvider(
            WarehouseProviderBuilder warehouseProviderBuilder,
            IWarehouseContainer warehouseContainer)
            : base(warehouseProviderBuilder) => warehouseProviderBuilder.WithWarehouseItems(warehouseContainer.WarehouseItems);

        IEnumerable<WarehouseItemDto> IWarehouseProvider.GetWarehouseItems() => GetService(r =>
        {
            Task.Delay(_random.Next(2000));
            return r;
        }).GetWarehouseItems();

        bool IWarehouseProvider.DeleteWarehouseItem(int id) => GetService(r =>
        {
            Task.Delay(_random.Next(2000));
            return r;
        }).DeleteWarehouseItem(id);

        bool IWarehouseProvider.UpdateWarehouseItem(WarehouseItemDto dto) => GetService(r =>
        {
            var delayTask = Task.Delay(_random.Next(2000));
            delayTask.Wait();
            return r;
        }).UpdateWarehouseItem(dto);

        void IWarehouseProvider.CreateWarehouseItem(WarehouseItemDto dto) => GetService(r =>
        {
            var delayTask = Task.Delay(_random.Next(2000));
            delayTask.Wait();
            return r;
        }).CreateWarehouseItem(dto);
    }
}
