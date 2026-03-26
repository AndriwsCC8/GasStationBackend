using System;

namespace GasStation.Application.DTOs;

public class ReceptionDto
{
    public int Id { get; set; }
    public int FuelId { get; set; }
    public decimal Quantity { get; set; }
    public DateTime Date { get; set; }
    public string? Observation { get; set; }
    public int UserId { get; set; }
}

public class CreateReceptionDto
{
    public int FuelId { get; set; }
    public decimal Quantity { get; set; }
    public string? Observation { get; set; }
    public int UserId { get; set; } // En un caso real se tomaría del Token JWT
}
