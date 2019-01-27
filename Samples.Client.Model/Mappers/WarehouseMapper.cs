using AutoMapper;
using Samples.Client.Data.Contracts.Dto;
using Samples.Client.Model.Contracts;

namespace Samples.Client.Model.Mappers
{
    static class WarehouseMapper
    {
        internal static IWarehouseItem MapToWarehouseItem(WarehouseItemDto warehouseItemDto)
        {
            var mapToWarehouseItem = Mapper.Map<WarehouseItem>(warehouseItemDto);
            mapToWarehouseItem.CommitChanges();
            return mapToWarehouseItem;
        }

        internal static WarehouseItemDto MapToWarehouseDto(IWarehouseItem warehouseItem)
        {
            return Mapper.Map<WarehouseItemDto>(warehouseItem);
        }
    }
}
