using AutoMapper;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Dto;
using LogoFX.Samples.Specifications.Client.Model.Contracts;

namespace LogoFX.Samples.Specifications.Client.Model.Mappers
{
    static class WarehouseMapper
    {
        internal static IWarehouseItem MapToWarehouseItem(WarehouseItemDto warehouseItemDto)
        {
            return Mapper.Map<WarehouseItem>(warehouseItemDto);
        }
    }
}
