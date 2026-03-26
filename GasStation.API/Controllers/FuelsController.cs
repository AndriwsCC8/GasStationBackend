using System.Threading.Tasks;
using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FuelsController : ControllerBase
{
    private readonly IFuelService _fuelService;

    public FuelsController(IFuelService fuelService)
    {
        _fuelService = fuelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _fuelService.GetAllAsync());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateFuelDto dto)
    {
        var result = await _fuelService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateFuelDto dto)
    {
        var result = await _fuelService.UpdateAsync(id, dto);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _fuelService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
