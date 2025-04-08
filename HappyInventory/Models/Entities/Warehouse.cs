namespace HappyInventory.API.Models.Entities;

public class Warehouse
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public ICollection<Item> Items { get; set; }
}