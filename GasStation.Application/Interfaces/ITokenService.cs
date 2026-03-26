using GasStation.Domain.Entities;

namespace GasStation.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
