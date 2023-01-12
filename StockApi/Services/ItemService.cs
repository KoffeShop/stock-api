using StockApi.Models;

namespace StockApi.Services;

public class ItemService
{
    private readonly Dictionary<string, Item> _storage = new();

    public void CreateItem(Item item)
    {
        _storage.TryAdd(item.Barcode, item);
    }

    public Item GetItem(string barcode)
    {
        return _storage[barcode];
    }

    public Item UpdateItem(Item item)
    {
        return _storage[item.Barcode] = item;
    }
    
}