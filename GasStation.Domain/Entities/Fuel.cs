namespace GasStation.Domain.Entities;

public class Fuel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;

    // Relaciones
    public Inventory? Inventory { get; set; }
}
