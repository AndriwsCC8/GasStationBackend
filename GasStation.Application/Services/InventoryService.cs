using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Application.Services;

public class InventoryService : IInventoryService
{
    private readonly IRepository<Inventory> _inventoryRepository;
    private readonly IRepository<Fuel> _fuelRepository;

    public InventoryService(IRepository<Inventory> inventoryRepository, IRepository<Fuel> fuelRepository)
    {
        _inventoryRepository = inventoryRepository;
        _fuelRepository = fuelRepository;
    }

    public async Task<IEnumerable<InventoryDto>> GetAllAsync()
    {
        var inventories = await _inventoryRepository.GetAllAsync();
        var fuels = await _fuelRepository.GetAllAsync();

        return inventories.Select(i => new InventoryDto
        {
            Id = i.Id,
            FuelId = i.FuelId,
            FuelName = fuels.FirstOrDefault(f => f.Id == i.FuelId)?.Name ?? "Desconocido",
            Stock = i.Stock
        });
    }
}
