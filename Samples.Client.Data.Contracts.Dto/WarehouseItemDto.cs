using System;

namespace Samples.Client.Data.Contracts.Dto
{
    [Serializable]
    public sealed class WarehouseItemDto
    {
        public string Kind { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
