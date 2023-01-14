using Dapper;
using StockApi.Models;

namespace StockApi.Repository;

public interface IItemsRepository
{
    Task CreateItem(Item item);
    Task<Item?> GetItem(string barcode);
    Task<IEnumerable<Item>> GetItemsById(int categoryId);
    Task UpdateItem(Item item);
    Task RemoveItem(string barcode);
}