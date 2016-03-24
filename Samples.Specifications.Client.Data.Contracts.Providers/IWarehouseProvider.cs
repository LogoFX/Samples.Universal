using System.Collections.Generic;
using System.Threading.Tasks;
using LogoFX.Samples.Specifications.Client.Data.Contracts.Dto;

namespace LogoFX.Samples.Specifications.Client.Data.Contracts.Providers
{
    public interface IWarehouseProvider
    {
        Task<IEnumerable<WarehouseItemDto>> GetWarehouseItems();
    }
}
