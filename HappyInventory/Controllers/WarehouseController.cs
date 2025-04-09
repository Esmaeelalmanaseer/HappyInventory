using HappyInventory.API.Helper.ResponseAPI;
using HappyInventory.API.Models.DTOs.Warehouse;
using HappyInventory.API.Models.Sharing;
using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    private readonly ILogger<WarehouseController> _logger;

    public WarehouseController(
        IWarehouseService warehouseService,
        ILogger<WarehouseController> logger)
    {
        _warehouseService = warehouseService;
        _logger = logger;
    }

    [HttpGet("get-all-Warehouse")]
    public async Task<IActionResult> GetAll([FromQuery]WarehouseParams WarehouseParams)
    {
        try
        {
            List<WarehouseResponseDto?> lstWarehouses = await _warehouseService.GetAllAsync(WarehouseParams);
            return lstWarehouses is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(lstWarehouses);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in GetAllWarehouses");
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            WarehouseResponseDto? warehouse = await _warehouseService.GetByConditionAsync(x => x.Id == id);
            return warehouse is null
                ? NotFound(new ResponseAPI(404))
                : Ok(warehouse);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in GetWarehouseById for ID {WarehouseId}", id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpPost("Add-Warehouse")]
    public async Task<IActionResult> Add(WarehouseCreateDto warehouseCreateDto)
    {
        try
        {
            WarehouseResponseDto? warehouse = await _warehouseService.AddAsync(warehouseCreateDto);
            return warehouse is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Add Warehouse Successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in AddWarehouse with data {@WarehouseData}", warehouseCreateDto);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpPut("update-Warehouse")]
    public async Task<IActionResult> Update(WarehouseUpdateDto warehouseUpdateDto)
    {
        try
        {
            WarehouseResponseDto? warehouse = await _warehouseService.UpdateAsync(warehouseUpdateDto);
            return warehouse is null
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Warehouse Updated Successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in UpdateWarehouse for ID {WarehouseId}", warehouseUpdateDto.Id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }

    [HttpDelete("Delete-Warehouse/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            bool result = await _warehouseService.DeletAsync(id);
            return !result
                ? BadRequest(new ResponseAPI(400))
                : Ok(new ResponseAPI(200, "Warehouse Deleted Successfully"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception in DeleteWarehouse for ID {WarehouseId}", id);
            return StatusCode(500, new ResponseAPI(500, "Internal server error"));
        }
    }
}