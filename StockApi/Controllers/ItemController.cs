using Microsoft.AspNetCore.Mvc;
using StockApi.Exceptions;
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
        try
        {
            await _service.CreateItem(item);
            return Ok(item);
        }
        catch (AlreadyExistsException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("by-barcode/{barcode}")]
    public async Task<IActionResult> GetItem([FromRoute] string barcode)
    {
        return Ok(await _service.GetItem(barcode));
    }

    [HttpGet("by-categoryId/{categoryId}")]
    public async Task<IActionResult> GetItemsById(int categoryId)
    {
        return Ok(await _service.GetItemsById(categoryId));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateItem([FromBody] Item item)
    {
        await _service.UpdateItem(item);
        return Ok(item);
    }

    [HttpDelete("by-barcode/{barcode}")]
    public async Task<IActionResult> DeleteItem([FromRoute] string barcode)
    {
        await _service.RemoveItem(barcode);
        return Ok();
    }
}