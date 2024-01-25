using ForecastApi.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    
    private readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] WeatherForecastCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}
