using System;

namespace Samples.Client.Data.Contracts.Dto
{    
    public sealed class WarehouseItemDto
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
