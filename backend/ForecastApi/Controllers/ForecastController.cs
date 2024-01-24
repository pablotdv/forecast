using ForecastApi.ExternalServices.Weathers;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class ForecastController : ControllerBase
{
    private readonly ForecastServices _forecastServices;

    public ForecastController(ForecastServices forecastServices)
    {
        _forecastServices = forecastServices;
    }

    [HttpGet]
    public async Task<Forecast> GetForecastAsync(ForecastRequest request)
    {
        return await _forecastServices.GetForecastAsync(request);
    }
}