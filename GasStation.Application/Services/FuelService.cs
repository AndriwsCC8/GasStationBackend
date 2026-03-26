using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Application.Services;

public class FuelService : IFuelService
{
    private readonly IRepository<Fuel> _fuelRepository;
    private readonly IRepository<Inventory> _inventoryRepository;

    public FuelService(IRepository<Fuel> fuelRepository, IRepository<Inventory> inventoryRepository)
    {
        _fuelRepository = fuelRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<FuelDto>> GetAllAsync()
    {
        var fuels = await _fuelRepository.GetAllAsync();
        return fuels.Select(f => new FuelDto
        {
            Id = f.Id,
            Name = f.Name,
            Price = f.Price,
            IsActive = f.IsActive
        });
    }

    public async Task<FuelDto?> GetByIdAsync(int id)
    {
        var fuel = await _fuelRepository.GetByIdAsync(id);
        if (fuel == null) return null;

        return new FuelDto
        {
            Id = fuel.Id,
            Name = fuel.Name,
            Price = fuel.Price,
            IsActive = fuel.IsActive
        };
    }

    public async Task<FuelDto> CreateAsync(CreateFuelDto dto)
    {
        var fuel = new Fuel
        {
            Name = dto.Name,
            Price = dto.Price,
            IsActive = true
        };

        var created = await _fuelRepository.AddAsync(fuel);
        await _fuelRepository.SaveChangesAsync();

        // Crear inventario en 0
        await _inventoryRepository.AddAsync(new Inventory { FuelId = created.Id, Stock = 0 });
        await _inventoryRepository.SaveChangesAsync();

        return new FuelDto { Id = created.Id, Name = created.Name, Price = created.Price, IsActive = created.IsActive };
    }

    public async Task<FuelDto?> UpdateAsync(int id, UpdateFuelDto dto)
    {
        var fuel = await _fuelRepository.GetByIdAsync(id);
        if (fuel == null) return null;

        fuel.Name = dto.Name;
        fuel.Price = dto.Price;
        fuel.IsActive = dto.IsActive;

        await _fuelRepository.UpdateAsync(fuel);
        await _fuelRepository.SaveChangesAsync();

        return new FuelDto { Id = fuel.Id, Name = fuel.Name, Price = fuel.Price, IsActive = fuel.IsActive };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var fuel = await _fuelRepository.GetByIdAsync(id);
        if (fuel == null) return false;

        await _fuelRepository.DeleteAsync(fuel);
        await _fuelRepository.SaveChangesAsync();
        return true;
    }
}
