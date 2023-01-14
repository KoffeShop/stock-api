using Dapper;
using Npgsql;
using StockApi.Exceptions;
using StockApi.Models;

namespace StockApi.Repository;

public class ItemsRepository : IDisposable, IItemsRepository
{
    private readonly NpgsqlConnection _connection;

    public ItemsRepository(IConfiguration configuration)
    {
        _connection = new NpgsqlConnection(configuration.GetConnectionString("Postgresql"));
    }

    public async Task CreateItem(Item item)
    {
        try
        {
            const string query =
                "insert into items (barcode, price, category_id, name, description, weight) values (:Barcode, :Price, :CategoryId, :Name, :Description, :Weight);";
            await _connection.ExecuteAsync(query, item);
        }
        catch(PostgresException exception) when (exception.Message.Contains(" повторяющееся значение ключа нарушает ограничение уникальности \"items_pk\""))
        {
            throw new AlreadyExistsException("Уже есть");
        }
    }

    public async Task<Item?> GetItem(string barcode)
    {
        const string query = "select * from items where barcode = :Barcode";
        var param = new { Barcode = barcode };
        return await _connection.QueryFirstOrDefaultAsync<Item>(query, param);
    }

    public async Task<IEnumerable<Item>> GetItemsById(int categoryId)
    {
        const string query = "select * from items where category_id = :CategoryId";
        var param = new { CategoryId = categoryId };
        return await _connection.QueryAsync<Item>(query, param);
    }

    public async Task UpdateItem(Item item)
    {
        const string query =
            "update items set price = :Price, category_id = :CategoryId, name = :Name, description = :Description, weight = :Weight where barcode = :Barcode;";
        await _connection.ExecuteAsync(query, item);
    }

    public async Task RemoveItem(string barcode)
    {
        const string query = "delete from items where barcode = :Barcode;";
        var param = new { Barcode = barcode };
        await _connection.ExecuteAsync(query, param);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}