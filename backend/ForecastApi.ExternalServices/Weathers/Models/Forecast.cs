using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class Forecast
{    
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}

