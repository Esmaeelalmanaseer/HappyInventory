using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.IRepositories;
using HappyInventory.API.Models.Sharing;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace HappyInventory.API.Data.Repositories;

public class ItemRepositry : IItemRepositry
{
    private readonly AppDbContext _dbContext;

    public ItemRepositry(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Item?> AddAsync(Item Itemobj)
    {
        await _dbContext.Items.AddAsync(Itemobj);
        await _dbContext.SaveChangesAsync();
        return Itemobj;
    }

    public async Task<bool> DeletAsync(int ItemobjID)
    {
        Item? ItemtOBJ = await _dbContext.Items.AsNoTracking().SingleOrDefaultAsync(x => x.Id == ItemobjID);
        if (ItemtOBJ is null)
        {
            return false;
        }
        _dbContext.Items.Remove(ItemtOBJ);
        int affectedRowsCount = await _dbContext.SaveChangesAsync();
        return affectedRowsCount > 0;
    }

    public async Task<IEnumerable<Item>> GetAllAsync(ItemParams ItemParams)
    {
        IQueryable<Item>? LstItem = _dbContext.Items.AsQueryable();

        if (ItemParams.WarehouseId.HasValue)
            LstItem= LstItem.Where(w => w.WarehouseId == ItemParams.WarehouseId);

        if (!string.IsNullOrEmpty(ItemParams.sort))
            LstItem= LstItem.Where(s => s.Name.Contains(ItemParams.sort) || s.SKUCode.Contains(ItemParams.sort));

        LstItem= LstItem.Skip((ItemParams.PageNumber - 1) * ItemParams.pageSize).Take(ItemParams.pageSize);

        return LstItem;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _dbContext.Items.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<Item?>> GetAllAsyncByConditionAsync(Expression<Func<Item, bool>> conditionExpression)
    {
        return await _dbContext.Items.AsNoTracking().Where(conditionExpression).ToListAsync();
    }

    public async Task<Item?> GetByConditionAsync(Expression<Func<Item, bool>> conditionExpression)
    {
        return await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(conditionExpression);
    }

    public async Task<IEnumerable<Item>> Order(Expression<Func<Item, object>> condition, bool Desc)
    {
        if(Desc)
            return await _dbContext.Items.AsNoTracking().OrderByDescending(condition).ToListAsync();
        return await _dbContext.Items.AsNoTracking().OrderBy(condition).ToListAsync();
    }

    public async Task<Item?> UpdateAsync(Item Itemobj)
    {
        Item? CheakObj = await _dbContext.Items.AsNoTracking().SingleOrDefaultAsync(x => x.Id == Itemobj.Id);
        if (CheakObj is null)
        {
            return null;
        }
        _dbContext.Update(Itemobj);
        _dbContext.SaveChanges();
        return Itemobj;
    }
}