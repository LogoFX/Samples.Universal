using System.Collections.Generic;
using System.Threading.Tasks;
using Samples.Universal.Client.Data.Contracts.Dto;

namespace Samples.Universal.Client.Data.Contracts.Providers
{
    public interface IWarehouseProvider
    {
        Task<IEnumerable<WarehouseItemDto>> GetWarehouseItems();
    }
}
