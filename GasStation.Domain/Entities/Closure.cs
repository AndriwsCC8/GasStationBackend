using System;

namespace GasStation.Domain.Entities;

public class Closure
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public decimal Differences { get; set; }

    // Relaciones
    public int UserId { get; set; }
    public User? User { get; set; }
}
