namespace StockApi.Models;

public class Item
{
    public string Barcode { get; set; }
    public long CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Weight { get; set; }
    public decimal Price { get; set; }
}