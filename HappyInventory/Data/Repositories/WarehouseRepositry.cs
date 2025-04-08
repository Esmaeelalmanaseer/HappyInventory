using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HappyInventory.API.Data.Repositories;
public class WarehouseRepositry : IWarehouseRepositry
{
    private readonly AppDbContext _dbContext;

    public WarehouseRepositry(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Warehouse?> AddAsync(Warehouse Warehouseobj)
    {
        await _dbContext.Warehouses.AddAsync(Warehouseobj);
        await _dbContext.SaveChangesAsync();
        return Warehouseobj;
    }

    public async Task<bool> DeletAsync(int WarehouseobjID)
    {
        Warehouse? productOBJ = await _dbContext.Warehouses.AsNoTracking().SingleOrDefaultAsync(x => x.Id == WarehouseobjID);
        if (productOBJ is null)
        {
            return false;
        }
        _dbContext.Warehouses.Remove(productOBJ);
        int affectedRowsCount = await _dbContext.SaveChangesAsync();
        return affectedRowsCount > 0;
    }

    public async Task<IEnumerable<Warehouse>> GetAllAsync()
    {
        return await _dbContext.Warehouses.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Warehouse?>> GetAllAsyncByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression)
    {
        return await _dbContext.Warehouses.AsNoTracking().Where(conditionExpression).ToListAsync();
    }

    public async Task<Warehouse?> GetByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression)
    {
        return await _dbContext.Warehouses.AsNoTracking().FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<Warehouse?> UpdateAsync(Warehouse Warehouseobj)
    {
        Warehouse? CheakObj = await _dbContext.Warehouses.AsNoTracking().SingleOrDefaultAsync(x=>x.Id == Warehouseobj.Id);
        if(CheakObj is null)
        {
            return null;
        }
        _dbContext.Update(Warehouseobj);
        _dbContext.SaveChanges();
        return Warehouseobj;
    }
}
