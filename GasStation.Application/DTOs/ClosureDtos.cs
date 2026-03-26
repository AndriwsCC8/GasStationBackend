using System;

namespace GasStation.Application.DTOs;

public class ClosureDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalSales { get; set; }
    public decimal Differences { get; set; }
    public int UserId { get; set; }
}

public class CreateClosureDto
{
    public DateTime Date { get; set; }
    public decimal SystemTotal { get; set; }
    public decimal ActualTotal { get; set; }
    public int UserId { get; set; }
}
