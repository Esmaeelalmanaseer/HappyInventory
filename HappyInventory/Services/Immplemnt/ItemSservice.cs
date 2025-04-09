using AutoMapper;
using HappyInventory.API.Models.DTOs.Item;
using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.IRepositories;
using HappyInventory.API.Models.Sharing;
using HappyInventory.API.Services.Interfaces;
using System.Linq.Expressions;

namespace HappyInventory.API.Services.Immplemnt;

public class ItemSservice : IItemSservice
{
    private readonly IItemRepositry _itemRepositry;
    private readonly IMapper _mapper;
    public ItemSservice(IItemRepositry itemRepositry, IMapper mapper)
    {
        _itemRepositry = itemRepositry;
        _mapper = mapper;
    }

    public async Task<ItemResponseDto?> AddAsync(ItemCreateDto Itemobj)
    {
        if (Itemobj is null)
        {
            throw new ArgumentNullException(nameof(Itemobj));
        }
        Item itemobj = _mapper.Map<Item>(Itemobj);
        var ItemResponse = await _itemRepositry.AddAsync(itemobj);
        if (ItemResponse is null)
        {
            return null;
        }
        return _mapper.Map<ItemResponseDto>(ItemResponse);
    }

    public async Task<bool> DeletAsync(int ItemobjID)
    {
        Item? Itemobj = await _itemRepositry.GetByConditionAsync(x => x.Id == ItemobjID);
        if (Itemobj is null) return false;
        return await _itemRepositry.DeletAsync(ItemobjID);
    }

    public async Task<List<ItemResponseDto?>> GetAllAsync(ItemParams ItemParams)
    {
        IEnumerable<Item> LstItems = await _itemRepositry.GetAllAsync(ItemParams);
        if (LstItems.Any())
        {
            IEnumerable<ItemResponseDto> LstItemREsponse = _mapper.Map<IEnumerable<ItemResponseDto>>(LstItems);
            return LstItemREsponse.ToList()!;
        }
        return null!;
    }

    public async Task<List<ItemResponseDto?>> GetAllAsyncByConditionAsync(Expression<Func<Item, bool>> conditionExpression)
    {
        IEnumerable<Item?> LstItem = await _itemRepositry.GetAllAsyncByConditionAsync(conditionExpression);

        IEnumerable<ItemResponseDto?> LstItemREsponse = _mapper.Map<IEnumerable<ItemResponseDto>>(LstItem);
        return LstItemREsponse.ToList();
    }

    public async Task<ItemResponseDto?> GetByConditionAsync(Expression<Func<Item, bool>> conditionExpression)
    {
        Item? ItemObj = await _itemRepositry.GetByConditionAsync(conditionExpression);
        if (ItemObj is null) throw new ArgumentException("Invalid Item Exiption");
        return _mapper.Map<ItemResponseDto>(ItemObj);
    }

    public async Task<ItemResponseDto?> UpdateAsync(ItemUpdateDto Itemobj)
    {
        Item? ItemObj = await _itemRepositry.GetByConditionAsync(x => x.Id == Itemobj.Id);
        if (ItemObj is null) throw new ArgumentException("Invalid Item Exiption");
        Item ItemUpdate = _mapper.Map<Item>(Itemobj);
        Item? Itemresponse = await _itemRepositry.UpdateAsync(ItemUpdate);
        return _mapper.Map<ItemResponseDto>(Itemresponse);
    }
}