using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces;

public interface ISaleService
{
    Task<IEnumerable<SaleDto>> GetAllAsync();
    Task<SaleDto> CreateAsync(CreateSaleDto dto);
}
