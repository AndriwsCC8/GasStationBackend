using System.Collections.Generic;
using System.Threading.Tasks;
using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces;

public interface IReceptionService
{
    Task<IEnumerable<ReceptionDto>> GetAllAsync();
    Task<ReceptionDto> CreateAsync(CreateReceptionDto dto);
}
