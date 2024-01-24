using ForecastApi.ExternalServices;
using ForecastApi.ExternalServices.Weathers;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class PointController : ControllerBase
{
    private readonly PointServices _pointServices;

    public PointController(PointServices pointServices)
    {
        _pointServices = pointServices;
    }

    [HttpGet]
    public async Task<Point> GetPointAsync(PointRequest request)
    {
        return await _pointServices.GetPointAsync(request);
    }
}
