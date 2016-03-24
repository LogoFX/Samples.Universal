using System;
using System.Collections.Generic;
using LogoFX.Client.Data.Fake.ProviderBuilders;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Dto;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Providers;

namespace LogoFX.Samples.Specifications.Client.Data.Fake.ProviderBuilders
{
    [Serializable]
    public class WarehouseProviderBuilder : FakeBuilderBase<IWarehouseProvider>
    {
        private readonly List<WarehouseItemDto> _warehouseItemsStorage = new List<WarehouseItemDto>();

        private WarehouseProviderBuilder()
        {
            
        }

        public static WarehouseProviderBuilder CreateBuilder()
        {
            return new WarehouseProviderBuilder();
        }

        public void WithWarehouseItems(IEnumerable<WarehouseItemDto> warehouseItems)
        {
            _warehouseItemsStorage.Clear();
            _warehouseItemsStorage.AddRange(warehouseItems);
        }

        protected override void SetupFake()
        {
            var initialSetup = CreateInitialSetup();

            var setup = initialSetup
                .AddMethodCallWithResultAsync(t => t.GetWarehouseItems(),
                    r => r.Complete(GetWarehouseItems)); 
           
            setup.Build();
        }

        private IEnumerable<WarehouseItemDto> GetWarehouseItems()
        {
            return _warehouseItemsStorage;
        }
    }
}
