using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using technicianMicroservice.Application.Services;
using technicianMicroservice.Domain.Entities;
using technicianMicroservice.Domain.Services;
using technicianMicroservice.DTOs;

namespace technicianMicroserviceAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnicianController : ControllerBase
{
    private readonly TechnicianService _technicianService;
    private readonly IValidator<technicianMicroservice.Domain.Entities.Technician> _validator;

    public TechnicianController(
        TechnicianService technicianService,
        IValidator<technicianMicroservice.Domain.Entities.Technician> validator)
    {
        _technicianService = technicianService;
        _validator = validator;
    }

    [Authorize(Roles = "Manager")]
    [HttpPost("create")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Insert(
        [FromBody] CreateTechnicianDto dto,
        [FromHeader] int userId)
    {
        var tech = new technicianMicroservice.Domain.Entities.Technician
        {
            Name = dto.Name,
            FirstLastName = dto.FirstLastName,
            SecondLastName = dto.SecondLastName,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            DocumentNumber = dto.DocumentNumber,
            DocumentExtension = dto.DocumentExtension,
            Address = dto.Address,
            BaseSalary = dto.BaseSalary
        };

        var validation = _validator.Validate(tech);
        if (validation.IsFailure)
        {
            return BadRequest(new ValidationErrorResponse
            {
                Message = "Validación fallida",
                Errors = validation.Errors
            });
        }

        var created = await _technicianService.Create(tech, userId);
        if (!created) return StatusCode(500, new { message = "Error al crear el técnico" });

        return CreatedAtAction(nameof(GetById), new { id = tech.Id }, new SuccessResponse
        {
            Message = "Técnico creado exitosamente",
            Id = tech.Id
        });
    }

    [Authorize(Roles = "Manager")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<technicianMicroservice.Domain.Entities.Technician>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Select()
    {
        var list = await _technicianService.GetAll();
        return Ok(list);
    }

    [Authorize(Roles = "Manager")]
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(technicianMicroservice.Domain.Entities.Technician), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var tech = await _technicianService.GetById(id);
        if (tech is null) return NotFound(new { message = $"Técnico con ID {id} no encontrado" });
        return Ok(tech);
    }

    [Authorize(Roles = "Manager")]
    [HttpPut()]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromBody] Technician technician,[FromHeader] int userId)
    {
        var existing = await _technicianService.GetById(technician.Id);
        if (existing is null) return NotFound(new { message = $"Técnico con ID {technician.Id} no encontrado" });

        var validation = _validator.Validate(technician);
        if (validation.IsFailure)
        {
            return BadRequest(new ValidationErrorResponse
            {
                Message = "Validación fallida",
                Errors = validation.Errors
            });
        }

        var ok = await _technicianService.Update(technician, userId);
        if (!ok) return StatusCode(500, new { message = "Error al actualizar el técnico" });

        return Ok(new SuccessResponse { Message = "Técnico actualizado exitosamente", Id = technician.Id });
    }

    [Authorize(Roles = "Manager")]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(
        int id,
        [FromHeader(Name = "User-Id")] int userId)
    {
        var existing = await _technicianService.GetById(id);
        if (existing is null) return NotFound(new { message = $"Técnico con ID {id} no encontrado" });

        var ok = await _technicianService.DeleteById(id, userId);
        if (!ok) return NotFound(new { message = "Técnico no encontrado o ya está inactivo" });

        return Ok(new { message = "Técnico desactivado exitosamente" });
    }
}
