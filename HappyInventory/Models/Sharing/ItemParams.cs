namespace HappyInventory.API.Models.Sharing;

public class ItemParams
{
    public string? sort { get; set; }
    public int? WarehouseId { get; set; }
    public int MaxPageSize { get; set; } = 10;
    public string? Search { get; set; }
    private int _PageSize = 10;

    public int pageSize
    {
        get { return _PageSize = 10; }
        set { _PageSize = value > MaxPageSize ? MaxPageSize : value; }
    }

    public int PageNumber { get; set; } = 1;
}
