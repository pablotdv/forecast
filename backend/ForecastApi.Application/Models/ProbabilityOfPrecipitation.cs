using System.Text.Json.Serialization;

namespace ForecastApi.Application.Models;

public class ProbabilityOfPrecipitation
{
    [JsonPropertyName("unitCode")]
    public string UnitCode { get; set; }

    [JsonPropertyName("value")]
    public double? Value { get; set; }
}

