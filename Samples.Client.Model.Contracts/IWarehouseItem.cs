namespace Samples.Client.Model.Contracts
{
    public interface IWarehouseItem : IAppModel
    {
        string Kind { get; set; }   
        double Price { get; set; }
        int Quantity { get; set; }
        double TotalCost { get; }
    }
}