using System.ComponentModel.DataAnnotations;

namespace GasStation.Application.DTOs;

public class FuelDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}

public class CreateFuelDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, 10000)]
    public decimal Price { get; set; }
}

public class UpdateFuelDto : CreateFuelDto
{
    public bool IsActive { get; set; }
}
