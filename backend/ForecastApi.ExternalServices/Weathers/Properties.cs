using System.Text.Json.Serialization;


namespace ForecastApi.ExternalServices.Weathers;

public class Properties
{
    [JsonPropertyName("updated")]
    public string Updated { get; set; }

    [JsonPropertyName("units")]
    public string Units { get; set; }

    [JsonPropertyName("forecastGenerator")]
    public string ForecastGenerator { get; set; }

    [JsonPropertyName("generatedAt")]
    public string GeneratedAt { get; set; }

    [JsonPropertyName("updateTime")]
    public string UpdateTime { get; set; }

    [JsonPropertyName("validTimes")]
    public string ValidTimes { get; set; }

    [JsonPropertyName("elevation")]
    public Elevation Elevation { get; set; }

    [JsonPropertyName("periods")]
    public List<Period> Periods { get; set; }
}

