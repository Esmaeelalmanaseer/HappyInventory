﻿using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.Sharing;
using System.Linq.Expressions;

namespace HappyInventory.API.Models.IRepositories;

public interface IItemRepositry
{
    Task<IEnumerable<Item>> GetAllAsync();

    Task<IEnumerable< Item>> GetAllAsync(ItemParams ItemParams);


    Task<IEnumerable< Item?>> GetAllAsyncByConditionAsync(Expression<Func< Item, bool>> conditionExpression);


    Task< Item?> GetByConditionAsync(Expression<Func< Item, bool>> conditionExpression);


    Task< Item?> AddAsync( Item  Itemobj);


    Task< Item?> UpdateAsync( Item  Itemobj);


    Task<bool> DeletAsync(int  ItemobjID);

    Task<IEnumerable<Item>> Order(Expression<Func<Item, object>> condition, bool Desc);
}