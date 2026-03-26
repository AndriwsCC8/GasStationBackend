using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces;

public interface IInventoryService
{
    Task<IEnumerable<InventoryDto>> GetAllAsync();
}
