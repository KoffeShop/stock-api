using Dapper;
using Npgsql;
using StockApi.Repository;

namespace StockApi.Migrations;

public class CategoryMigration
{
    private const string Query = """
                                    create table category (id int primary key , name text);
                                    alter table items
                                    add column category_id int,
                                    add column name text,
                                    add column description text,
                                    add column weight decimal
                                 """;

    public void Migrate(IConfiguration configuration)
    {
        var connection = new NpgsqlConnection(configuration.GetConnectionString("Postgresql"));
        connection.Execute(Query);
    }
}