using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces;

public interface IClosureService
{
    Task<IEnumerable<ClosureDto>> GetAllAsync();
    Task<ClosureDto> CreateAsync(CreateClosureDto dto);
}
