using System.Collections.Generic;
using Samples.Client.Data.Contracts.Dto;

namespace Samples.Client.Data.Contracts.Providers
{
    public interface IWarehouseProvider
    {
        IEnumerable<WarehouseItemDto> GetWarehouseItems();
        bool DeleteWarehouseItem(int id);
        bool UpdateWarehouseItem(WarehouseItemDto dto);
        void CreateWarehouseItem(WarehouseItemDto dto);
    }
}
