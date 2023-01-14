using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StockApi.Models;

namespace StockApi.Controllers;

[ApiController]
[Route("proxy")]
public class ProxyController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ProxyController(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient();
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        using var response = await _httpClient.GetAsync($"posts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _httpClient.GetFromJsonAsync<Content[]>("posts/");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Content content)
    {
        var response = await _httpClient.PostAsJsonAsync("posts", content);
        return Ok(await response.Content.ReadFromJsonAsync<Content>());
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Content content)
    {
        var response = await _httpClient.PutAsJsonAsync($"posts/{content.Id}", content);
        return Ok(await response.Content.ReadFromJsonAsync<Content>());
    }

    [HttpDelete]
    public async void Delete([FromRoute] int id)
    {
        using var response = await _httpClient.DeleteAsync($"posts/{id}");
    }
}