using System;
using System.Linq;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Entities;
using BCrypt.Net;

namespace GasStation.Application.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IRepository<User> userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var users = await _userRepository.GetAllAsync();
        var user = users.FirstOrDefault(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        var token = _tokenService.GenerateToken(user);

        return new LoginResponse
        {
            Token = token,
            Username = user.Username,
            Role = user.Role.ToString()
        };
    }
}
