using Microsoft.AspNetCore.Mvc;
using StockApi.Models;
using StockApi.Services;

namespace StockApi.Controllers;

[ApiController]
[Route("items")]
public class ItemController : ControllerBase
{
    private readonly ItemService _service;

    public ItemController(ItemService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem([FromBody] Item item)
    {
        _service.CreateItem(item);
        await Task.Delay(1);
        return Ok(item);
    }

    [HttpGet("by-barcode/{barcode}")]
    public async Task<IActionResult> GetItem([FromRoute] string barcode)
    {
        await Task.Delay(1);
        return Ok(_service.GetItem(barcode));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateItem([FromBody] Item item)
    {
        _service.UpdateItem(item);
        await Task.Delay(1);
        return Ok(item);
    }

    [HttpDelete("by-barcode/{barcode}")]
    public async Task<IActionResult> DeleteItem([FromRoute] string barcode)
    {
        await Task.Delay(1);
        _service.RemoveItem(barcode);
        return Ok();
    }
}