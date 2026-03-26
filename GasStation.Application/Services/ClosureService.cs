using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;

namespace GasStation.Application.Services;

public class ClosureService : IClosureService
{
    private readonly IRepository<Closure> _closureRepository;
    private readonly IRepository<Sale> _saleRepository;

    public ClosureService(IRepository<Closure> closureRepository, IRepository<Sale> saleRepository)
    {
        _closureRepository = closureRepository;
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<ClosureDto>> GetAllAsync()
    {
        var closures = await _closureRepository.GetAllAsync();
        return closures.Select(c => new ClosureDto
        {
            Id = c.Id,
            Date = c.Date,
            TotalSales = c.TotalSales,
            Differences = c.Differences,
            UserId = c.UserId
        });
    }

    public async Task<ClosureDto> CreateAsync(CreateClosureDto dto)
    {
        var dateOnly = dto.Date.Date;

        // Verificar si ya existe cierre para esa fecha
        var closures = await _closureRepository.GetAllAsync();
        if (closures.Any(c => c.Date.Date == dateOnly))
        {
            throw new Exception($"Ya existe un cierre para la fecha {dateOnly:yyyy-MM-dd}");
        }

        // Calcular ventas del día
        var allSales = await _saleRepository.GetAllAsync();
        var dailySales = allSales.Where(s => s.Date.Date == dateOnly).Sum(s => s.Total);

        // Si el dto.SystemTotal es diferente, podríamos validar.
        // Asumimos que dto.SystemTotal es una verificación de la vista del usuario, 
        // pero mandamos el cálculo real del sistema o usamos el calculado.
        var differences = dto.ActualTotal - dailySales;

        var closure = new Closure
        {
            Date = dateOnly,
            TotalSales = dailySales,
            Differences = differences,
            UserId = dto.UserId
        };

        var created = await _closureRepository.AddAsync(closure);
        await _closureRepository.SaveChangesAsync();

        return new ClosureDto
        {
            Id = created.Id,
            Date = created.Date,
            TotalSales = created.TotalSales,
            Differences = created.Differences,
            UserId = created.UserId
        };
    }
}
