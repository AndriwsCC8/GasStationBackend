using System.Security.Claims;
using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReceptionsController : ControllerBase
{
    private readonly IReceptionService _receptionService;

    public ReceptionsController(IReceptionService receptionService)
    {
        _receptionService = receptionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _receptionService.GetAllAsync());
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supervisor")]
    public async Task<IActionResult> Create([FromBody] CreateReceptionDto dto)
    {
        // Obtener UserId del token si no viene explícitamente (o sobreescribir por seguridad)
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdString, out int userId))
        {
            dto.UserId = userId;
        }

        var result = await _receptionService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}
