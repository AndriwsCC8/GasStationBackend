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
public class ClosuresController : ControllerBase
{
    private readonly IClosureService _closureService;

    public ClosuresController(IClosureService closureService)
    {
        _closureService = closureService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _closureService.GetAllAsync());
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Supervisor")]
    public async Task<IActionResult> Create([FromBody] CreateClosureDto dto)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(userIdString, out int userId))
        {
            dto.UserId = userId;
        }

        try
        {
            var result = await _closureService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
