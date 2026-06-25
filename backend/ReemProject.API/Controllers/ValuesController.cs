using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReemProject.Application.Features.Values.Queries.GetValues;

namespace ReemProject.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ValuesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await mediator.Send(new GetValuesQuery());
        return Ok(result);
    }
}
