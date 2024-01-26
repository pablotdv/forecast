using ForecastApi.ExternalServices.Weathers;
using ForecastApi.ExternalServices.Weathers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class ForecastController : ControllerBase
{
    private readonly IForecastServices _forecastServices;

    public ForecastController(IForecastServices forecastServices)
    {
        _forecastServices = forecastServices;
    }

    [HttpGet]
    public async Task<Forecast> GetForecastAsync(ForecastRequest request)
    {
        return await _forecastServices.GetForecastAsync(request);
    }
}