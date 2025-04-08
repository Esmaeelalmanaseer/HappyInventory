using HappyInventory.API.Models.DTOs.Warehouse;
using HappyInventory.API.Models.Entities;
using System.Linq.Expressions;

namespace HappyInventory.API.Services.Interfaces;

public interface IWarehouseService
{
    Task<List<WarehouseResponseDto?>> GetAllAsync();


    Task<List<WarehouseResponseDto?>> GetAllAsyncByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression);


    Task<WarehouseResponseDto?> GetByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression);


    Task<WarehouseResponseDto?> AddAsync(WarehouseCreateDto Warehouseobj);


    Task<WarehouseResponseDto?> UpdateAsync(WarehouseUpdateDto Warehouseobj);


    Task<bool> DeletAsync(int WarehouseobjID);
}