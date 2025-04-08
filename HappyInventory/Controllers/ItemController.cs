using HappyInventory.API.Helper.ResponseAPI;
using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemSservice _itemService;
    private readonly ILogger<ItemController> _logger;

    public ItemController(
        IItemSservice itemService,
        ILogger<ItemController> logger)
    {
        _itemService = itemService;
        _logger = logger;
    }

    [HttpGet("get-all-items")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            List<ItemResponseDto?> items = await _itemService.GetAllAsync();
            return items is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in GetAllItems");
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            ItemResponseDto? item = await _itemService.GetByConditionAsync(x => x.Id == id);
            return item is null
                ? NotFound(new ResponseAPI(404))
                : Ok(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in GetItemById for ID {ItemId}", id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> Add(ItemCreateDto itemCreateDto)
    {
        try
        {
            ItemResponseDto? item = await _itemService.AddAsync(itemCreateDto);
            return item is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Item added successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in AddItem with data {@ItemData}", itemCreateDto);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpPut("update-item")]
    public async Task<IActionResult> Update(ItemUpdateDto itemUpdateDto)
    {
        try
        {
            ItemResponseDto? item = await _itemService.UpdateAsync(itemUpdateDto);
            return item is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Item updated successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in UpdateItem for ID {ItemId}", itemUpdateDto.Id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpDelete("delete-item/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            bool result = await _itemService.DeletAsync(id);
            return !result
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Item deleted successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in DeleteItem for ID {ItemId}", id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }
}