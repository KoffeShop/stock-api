using StockApi.Migrations;
using StockApi.Repository;
using StockApi.Services;

var builder = WebApplication.CreateBuilder(args);

if (args.Length > 0 && args[0] == "migrate")
{
    var migration = new CategoryMigration();
    migration.Migrate(builder.Configuration);
}

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ItemService>();
builder.Services.AddScoped<IItemsRepository, ItemsRepository>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();