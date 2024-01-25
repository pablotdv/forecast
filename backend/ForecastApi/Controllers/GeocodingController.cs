using ForecastApi.ExternalServices;
using ForecastApi.ExternalServices.Geocodings;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class GeocodingController : ControllerBase
{
    private readonly GeocodingServices _geocodingServices;

    public GeocodingController(GeocodingServices geocodingServices)
    {
        _geocodingServices = geocodingServices;
    }

    [HttpGet]
    public async Task<RootObject> GetGeocodingAsync(GeocodingRequest request)
    {
        return await _geocodingServices.GetGeocodingAsync(request);
    }
}
