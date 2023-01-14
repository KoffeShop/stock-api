using Dapper;
using Npgsql;
using StockApi.Models;
using StockApi.Repository;

namespace StockApi.Services;

public class ItemService
{
    private readonly IItemsRepository _repository;

    public ItemService(IItemsRepository repository)
    {
        _repository = repository;
    }

    public Task CreateItem(Item item)
    {
        return _repository.CreateItem(item);
    }

    public Task<Item?> GetItem(string barcode)
    {
        return _repository.GetItem(barcode);
    }

    public Task<IEnumerable<Item>> GetItemsById(int categoryId)
    {
        return _repository.GetItemsById(categoryId);
    }

    public Task UpdateItem(Item item)
    {
        return _repository.UpdateItem(item);
    }

    public Task RemoveItem(string barcode)
    {
        return _repository.RemoveItem(barcode);
    }

}