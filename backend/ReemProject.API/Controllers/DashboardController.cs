using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReemProject.Application.Features.Dashboard.Queries.GetDashboardStats;
using System.Security.Claims;

namespace ReemProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var isAdmin = User.IsInRole("Admin");
        int? managerId = isAdmin ? null : int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await mediator.Send(new GetDashboardStatsQuery(managerId));
        return Ok(result);
    }
}
