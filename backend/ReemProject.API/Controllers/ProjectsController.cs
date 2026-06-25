using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReemProject.Application.Features.Projects.Commands.DeleteProject;
using ReemProject.Application.Features.Projects.Commands.UpsertProject;
using ReemProject.Application.Features.Projects.Queries.GetProjects;
using ReemProject.Domain.Enums;
using System.Security.Claims;

namespace ReemProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? valueId, [FromQuery] ProjectStatus? status)
    {
        var isAdmin = User.IsInRole("Admin");
        int? managerId = isAdmin ? null : int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await mediator.Send(new GetProjectsQuery(managerId, valueId, status));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Upsert([FromBody] UpsertProjectCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteProjectCommand(id));
        return NoContent();
    }
}
