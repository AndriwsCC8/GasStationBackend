using System;

namespace GasStation.Application.DTOs;

public class SaleDto
{
    public int Id { get; set; }
    public int FuelId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }
    public int UserId { get; set; }
}

public class CreateSaleDto
{
    public int FuelId { get; set; }
    public decimal Quantity { get; set; }
    public int UserId { get; set; } // En la realidad vendrá del User Claim
}
