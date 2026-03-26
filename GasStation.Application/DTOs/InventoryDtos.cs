namespace GasStation.Application.DTOs;

public class InventoryDto
{
    public int Id { get; set; }
    public int FuelId { get; set; }
    public string FuelName { get; set; } = string.Empty;
    public decimal Stock { get; set; }
}
