using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class Forecast
{    
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; }
}

