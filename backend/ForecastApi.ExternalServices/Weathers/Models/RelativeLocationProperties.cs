using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class RelativeLocationProperties
{
    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("distance")]
    public Distance Distance { get; set; }

    [JsonPropertyName("bearing")]
    public Bearing Bearing { get; set; }
}
