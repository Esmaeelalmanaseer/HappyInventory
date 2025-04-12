using AutoMapper;
using HappyInventory.API.Models.DTOs.Warehouse;
using HappyInventory.API.Models.Entities;
using HappyInventory.API.Models.IRepositories;
using HappyInventory.API.Models.Sharing;
using HappyInventory.API.Services.Interfaces;
using System.Linq.Expressions;

namespace HappyInventory.API.Services.Immplemnt;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepositry _warehouseRepositry;
    private readonly IMapper _mapper;
    public WarehouseService(IWarehouseRepositry warehouseRepositry, IMapper mapper)
    {
        _warehouseRepositry = warehouseRepositry;
        _mapper = mapper;
    }

    public async Task<WarehouseResponseDto?> AddAsync(WarehouseCreateDto Warehouseobj)
    {
        if (Warehouseobj is null)
        {
            throw new ArgumentNullException(nameof(Warehouseobj));
        }
        Warehouse WarehouseAdd = _mapper.Map<Warehouse>(Warehouseobj);
        Warehouse? WarehouseResponse = await _warehouseRepositry.AddAsync(WarehouseAdd);
        if (WarehouseResponse is null)
        {
            return null;
        }
        return _mapper.Map<WarehouseResponseDto>(WarehouseResponse);
    }

    public async Task<bool> DeletAsync(int WarehouseobjID)
    {
        Warehouse? Warehouseobj = await _warehouseRepositry.GetByConditionAsync(x => x.Id == WarehouseobjID);
        if (Warehouseobj is null) return false;
        return await _warehouseRepositry.DeletAsync(WarehouseobjID);
    }

    public async Task<List<WarehouseResponseDto?>> GetAllAsync(WarehouseParams WarehouseParams)
    {
        IEnumerable<Warehouse> LstWarehouse = await _warehouseRepositry.GetAllAsync(WarehouseParams);
        if (LstWarehouse.Any())
        {
            IEnumerable<WarehouseResponseDto> LsWarehousesponse = _mapper.Map<IEnumerable<WarehouseResponseDto>>(LstWarehouse);
            return LsWarehousesponse.ToList()!;
        }
        return null!;
    }

    public async Task<List<WarehouseResponseDto?>> GetAllAsync()
    {
        var LstWarehouse=await _warehouseRepositry.GetAllAsync();

        IEnumerable<WarehouseResponseDto> LstItemREsponse = _mapper.Map<IEnumerable<WarehouseResponseDto>>(LstWarehouse);
        return LstItemREsponse.ToList()!;
    }

    public async Task<List<WarehouseResponseDto?>> GetAllAsyncByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression)
    {
        IEnumerable<Warehouse?> LstWarehouse = await _warehouseRepositry.GetAllAsyncByConditionAsync(conditionExpression);

        IEnumerable<WarehouseResponseDto?> LstIWarehouseResponse = _mapper.Map<IEnumerable<WarehouseResponseDto>>(LstWarehouse);
        return LstIWarehouseResponse.ToList();
    }

    public async Task<WarehouseResponseDto?> GetByConditionAsync(Expression<Func<Warehouse, bool>> conditionExpression)
    {
        Warehouse? Warehousebj = await _warehouseRepositry.GetByConditionAsync(conditionExpression);
        if (Warehousebj is null) throw new ArgumentException("Invalid Warehouse Exiption");
        return _mapper.Map<WarehouseResponseDto>(Warehousebj);
    }

    public async Task<WarehouseResponseDto?> UpdateAsync(WarehouseUpdateDto Warehouseobj)
    {
        Warehouse? WarehouseObj = await _warehouseRepositry.GetByConditionAsync(x => x.Id == Warehouseobj.Id);
        if (WarehouseObj is null) throw new ArgumentException("Invalid Warehouse Exiption");
        Warehouse WarehouseUpdate = _mapper.Map<Warehouse>(Warehouseobj);
        Warehouse? Warehouseresponse = await _warehouseRepositry.UpdateAsync(WarehouseUpdate);
        return _mapper.Map<WarehouseResponseDto>(Warehouseresponse);
    }
}