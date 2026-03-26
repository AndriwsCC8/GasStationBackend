namespace GasStation.Domain.Entities;

public class Inventory
{
    public int Id { get; set; }
    public decimal Stock { get; set; }

    // Relaciones
    public int FuelId { get; set; }
    public Fuel? Fuel { get; set; }
}
