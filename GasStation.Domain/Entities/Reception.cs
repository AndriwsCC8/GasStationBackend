using System;

namespace GasStation.Domain.Entities;

public class Reception
{
    public int Id { get; set; }
    public decimal Quantity { get; set; }
    public DateTime Date { get; set; }
    public string? Observation { get; set; }

    // Relaciones
    public int FuelId { get; set; }
    public Fuel? Fuel { get; set; }

    public int UserId { get; set; }
    public User? User { get; set; }
}
