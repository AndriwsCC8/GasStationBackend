using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Application.Services;

public class ReceptionService : IReceptionService
{
    private readonly IRepository<Reception> _receptionRepository;
    private readonly IRepository<Inventory> _inventoryRepository;

    public ReceptionService(IRepository<Reception> receptionRepository, IRepository<Inventory> inventoryRepository)
    {
        _receptionRepository = receptionRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<ReceptionDto>> GetAllAsync()
    {
        var receptions = await _receptionRepository.GetAllAsync();
        return receptions.Select(r => new ReceptionDto
        {
            Id = r.Id,
            FuelId = r.FuelId,
            Quantity = r.Quantity,
            Date = r.Date,
            Observation = r.Observation,
            UserId = r.UserId
        });
    }

    public async Task<ReceptionDto> CreateAsync(CreateReceptionDto dto)
    {
        var reception = new Reception
        {
            FuelId = dto.FuelId,
            Quantity = dto.Quantity,
            Date = DateTime.UtcNow,
            Observation = dto.Observation,
            UserId = dto.UserId
        };

        var created = await _receptionRepository.AddAsync(reception);

        // Actualizar inventario
        var inventories = await _inventoryRepository.GetAllAsync();
        var inventory = inventories.FirstOrDefault(i => i.FuelId == dto.FuelId);

        if (inventory != null)
        {
            inventory.Stock += dto.Quantity;
            await _inventoryRepository.UpdateAsync(inventory);
        }
        else
        {
            await _inventoryRepository.AddAsync(new Inventory { FuelId = dto.FuelId, Stock = dto.Quantity });
        }

        await _receptionRepository.SaveChangesAsync(); // Guarda recepción y actualización de inventario

        return new ReceptionDto
        {
            Id = created.Id,
            FuelId = created.FuelId,
            Quantity = created.Quantity,
            Date = created.Date,
            Observation = created.Observation,
            UserId = created.UserId
        };
    }
}
