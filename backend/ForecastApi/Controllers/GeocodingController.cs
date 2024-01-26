using ForecastApi.ExternalServices;
using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Geocodings.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForecastApi;

[ApiController]
[Route("[controller]")]
public class GeocodingController : ControllerBase
{
    private readonly IGeocodingServices _geocodingServices;

    public GeocodingController(IGeocodingServices geocodingServices)
    {
        _geocodingServices = geocodingServices;
    }

    [HttpGet]
    public async Task<Geocoding> GetGeocodingAsync(GeocodingRequest request)
    {
        return await _geocodingServices.GetGeocodingAsync(request);
    }
}
