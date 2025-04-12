using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.Sharing;
using System.Linq.Expressions;

namespace HappyInventory.API.Models.IRepositories;

public interface IWarehouseRepositry
{
    Task<IEnumerable<Warehouse>> GetAllAsync();

    Task<IEnumerable<Warehouse>> GetAllAsync(WarehouseParams WarehouseParams);


    Task<IEnumerable<Warehouse?>> GetAllAsyncByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression);


    Task<Warehouse?> GetByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression);


    Task<Warehouse?> AddAsync(Warehouse Warehouseobj);


    Task<Warehouse?> UpdateAsync(Warehouse Warehouseobj);


    Task<bool> DeletAsync(int WarehouseobjID);
}
