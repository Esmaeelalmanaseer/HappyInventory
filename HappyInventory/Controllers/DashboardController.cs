using HappyInventory.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Management")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("warehouse-status")]
        public async Task<IActionResult> GetWarehouseStatus()
        {
            var result = await _dashboardService.GetWarehouseInventoryStatus();
            return Ok(result);
        }

        [HttpGet("top-high")]
        public async Task<IActionResult> GetTopHighItems()
        {
            var result = await _dashboardService.GetTopHighItems();
            return Ok(result);
        }

        [HttpGet("top-low")]
        public async Task<IActionResult> GetTopLowItems()
        {
            var result = await _dashboardService.GetTopLowItems();
            return Ok(result);
        }
    }
}
