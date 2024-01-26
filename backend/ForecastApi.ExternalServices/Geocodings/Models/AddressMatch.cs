using System.Text.Json.Serialization;

namespace ForecastApi.ExternalServices.Geocodings.Models;

public class AddressMatch
{
    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; }
}
