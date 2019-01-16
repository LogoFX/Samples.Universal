using System.Collections.Generic;
using System.Threading.Tasks;

namespace Samples.Client.Model.Contracts
{
    public interface IDataService
    {
        IEnumerable<IWarehouseItem> WarehouseItems { get; }

        Task<IWarehouseItem> NewWarehouseItemAsync();

        Task GetWarehouseItemsAsync();

        Task<bool> DeleteWarehouseItemAsync(IWarehouseItem item);

        Task SaveWarehouseItemAsync(IWarehouseItem item);
    }
}
