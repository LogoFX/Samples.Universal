using System.Collections.Generic;
using System.Threading.Tasks;
using Samples.Client.Data.Contracts.Dto;
using Samples.Client.Data.Contracts.Providers;

namespace Samples.Client.Data.Real.Providers
{
    class WarehouseProvider : IWarehouseProvider
    {
        public async Task<IEnumerable<WarehouseItemDto>> GetWarehouseItems()
        {
            return await Task.Run(() => new[]
            {
                new WarehouseItemDto
                {
                    Kind = "Good"
                }
            });
        }
    }
}