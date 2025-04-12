using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.Sharing;
using System.Linq.Expressions;

namespace HappyInventory.API.Services.Interfaces;

public interface IItemSservice
{

    Task<List<ItemResponseDto?>> GetAllAsync();

    Task<List<ItemResponseDto?>> GetAllAsync(ItemParams ItemParams);

    Task<List<ItemResponseDto?>> GetAllAsyncByConditionAsync(Expression<Func< Item, bool>> conditionExpression);


    Task<ItemResponseDto?> GetByConditionAsync(Expression<Func< Item, bool>> conditionExpression);


    Task<ItemResponseDto?> AddAsync(ItemCreateDto Itemobj);


    Task<ItemResponseDto?> UpdateAsync(ItemUpdateDto Itemobj);


    Task<bool> DeletAsync(int ItemobjID);

    Task<List<ItemResponseDto>> Order(Expression<Func<Item, object>> condition, bool Desc);
}