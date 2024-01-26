using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Geocodings.Models;
using ForecastApi.ExternalServices.Weathers;
using MediatR;

namespace ForecastApi.Application;

public class WeatherForecastCommandHandler : IRequestHandler<WeatherForecastCommand, WeatherForecastCommandResult>
{
    private readonly IGeocodingServices _geocodingServices;
    private readonly IForecastServices _forecastServices;
    private readonly IPointServices _pointServices;
    public WeatherForecastCommandHandler(IGeocodingServices geocodingServices, IForecastServices forecastServices, IPointServices pointServices)
    {
        _geocodingServices = geocodingServices;
        _forecastServices = forecastServices;
        _pointServices = pointServices;
    }

    public async Task<WeatherForecastCommandResult> Handle(WeatherForecastCommand request, CancellationToken cancellationToken)
    {
        var geocodingResponse = await _geocodingServices.GetGeocodingAsync(new GeocodingRequest
        {
            Street = request.Street,
            City = request.City,
            State = request.State,
            Zip = request.Zip
        });

        if (geocodingResponse?.Result?.AddressMatches?.Count == 0)
            return null;

        var pointResponse = await _pointServices.GetPointAsync(new PointRequest
        {
            Latitude = geocodingResponse.Result.AddressMatches.FirstOrDefault().Coordinates.Y,
            Longitude = geocodingResponse.Result.AddressMatches.FirstOrDefault().Coordinates.X
        });

        var forecastResponse = await _forecastServices.GetForecastAsync(new ForecastRequest
        {
            GridId = pointResponse.Properties.GridId,
            GridX = pointResponse.Properties.GridX,
            GridY = pointResponse.Properties.GridY
        });

        if (forecastResponse?.Properties?.Periods?.Count == 0)
            return null;

        return new WeatherForecastCommandResult
        {
            Forecast = forecastResponse.Properties.Periods.Select(
                period => new ForecastApi.Application.Models.Forecast {
                    Number = period.Number,
                    Name = period.Name,
                    StartTime = period.StartTime,
                    EndTime = period.EndTime,
                    IsDaytime = period.IsDaytime,
                    Temperature = period.Temperature,
                    TemperatureUnit = period.TemperatureUnit,
                    TemperatureTrend = period.TemperatureTrend,
                    ProbabilityOfPrecipitation = new Models.ProbabilityOfPrecipitation {
                        UnitCode = period.ProbabilityOfPrecipitation.UnitCode,
                        Value = period.ProbabilityOfPrecipitation.Value
                    },
                    Dewpoint = new Models.Dewpoint {
                        UnitCode = period.Dewpoint.UnitCode,
                        Value = period.Dewpoint.Value
                    },
                    RelativeHumidity = new Models.RelativeHumidity {
                        UnitCode = period.RelativeHumidity.UnitCode,
                        Value = period.RelativeHumidity.Value
                    },
                    WindSpeed = period.WindSpeed,
                    WindDirection = period.WindDirection,
                    Icon = period.Icon,
                    ShortForecast = period.ShortForecast,
                    DetailedForecast = period.DetailedForecast
                }
            ).ToList()
        };
    }
}

