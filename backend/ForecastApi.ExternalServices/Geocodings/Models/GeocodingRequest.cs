namespace ForecastApi.ExternalServices.Geocodings.Models;

public class GeocodingRequest
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
}
