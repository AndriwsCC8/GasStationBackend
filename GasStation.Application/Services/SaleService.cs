using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Application.Services;

public class SaleService : ISaleService
{
    private readonly IRepository<Sale> _saleRepository;
    private readonly IRepository<Fuel> _fuelRepository;
    private readonly IRepository<Inventory> _inventoryRepository;

    public SaleService(
        IRepository<Sale> saleRepository,
        IRepository<Fuel> fuelRepository,
        IRepository<Inventory> inventoryRepository)
    {
        _saleRepository = saleRepository;
        _fuelRepository = fuelRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<IEnumerable<SaleDto>> GetAllAsync()
    {
        var sales = await _saleRepository.GetAllAsync();
        return sales.Select(s => new SaleDto
        {
            Id = s.Id,
            FuelId = s.FuelId,
            Quantity = s.Quantity,
            Price = s.Price,
            Total = s.Total,
            Date = s.Date,
            UserId = s.UserId
        });
    }

    public async Task<SaleDto> CreateAsync(CreateSaleDto dto)
    {
        var fuel = await _fuelRepository.GetByIdAsync(dto.FuelId);
        if (fuel == null) throw new Exception("Combustible no encontrado");

        var inventories = await _inventoryRepository.GetAllAsync();
        var inventory = inventories.FirstOrDefault(i => i.FuelId == dto.FuelId);

        if (inventory == null || inventory.Stock < dto.Quantity)
        {
            throw new Exception("Inventario insuficiente");
        }

        var sale = new Sale
        {
            FuelId = dto.FuelId,
            Quantity = dto.Quantity,
            Price = fuel.Price,
            Total = dto.Quantity * fuel.Price,
            Date = DateTime.UtcNow,
            UserId = dto.UserId
        };

        var created = await _saleRepository.AddAsync(sale);

        // Descontar del inventario
        inventory.Stock -= dto.Quantity;
        await _inventoryRepository.UpdateAsync(inventory);

        await _saleRepository.SaveChangesAsync();

        return new SaleDto
        {
            Id = created.Id,
            FuelId = created.FuelId,
            Quantity = created.Quantity,
            Price = created.Price,
            Total = created.Total,
            Date = created.Date,
            UserId = created.UserId
        };
    }
}
