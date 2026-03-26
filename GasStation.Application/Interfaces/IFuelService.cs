using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces;

public interface IFuelService
{
    Task<IEnumerable<FuelDto>> GetAllAsync();
    Task<FuelDto?> GetByIdAsync(int id);
    Task<FuelDto> CreateAsync(CreateFuelDto dto);
    Task<FuelDto?> UpdateAsync(int id, UpdateFuelDto dto);
    Task<bool> DeleteAsync(int id);
}
