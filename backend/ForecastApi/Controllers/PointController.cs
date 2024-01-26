using ForecastApi.ExternalServices;
using ForecastApi.ExternalServices.Weathers;
using ForecastApi.ExternalServices.Weathers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class PointController : ControllerBase
{
    private readonly IPointServices _pointServices;

    public PointController(IPointServices pointServices)
    {
        _pointServices = pointServices;
    }

    [HttpGet]
    public async Task<Point> GetPointAsync(PointRequest request)
    {
        return await _pointServices.GetPointAsync(request);
    }
}
