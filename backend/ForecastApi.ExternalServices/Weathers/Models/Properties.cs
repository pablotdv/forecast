using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers.Models;

public class Properties
{    
    [JsonPropertyName("periods")]
    public List<Period> Periods { get; set; }
}

