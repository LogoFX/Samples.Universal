using AutoMapper;
using Samples.Universal.Client.Data.Contracts.Dto;
using Samples.Universal.Client.Model.Contracts;

namespace Samples.Universal.Client.Model.Mappers
{
    static class WarehouseMapper
    {
        internal static IWarehouseItem MapToWarehouseItem(WarehouseItemDto warehouseItemDto)
        {
            return Mapper.Map<WarehouseItem>(warehouseItemDto);
        }
    }
}
