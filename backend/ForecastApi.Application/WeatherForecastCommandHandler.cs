using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Weathers;
using MediatR;

namespace ForecastApi.Application;

public class WeatherForecastCommandHandler : IRequestHandler<WeatherForecastCommand, WeatherForecastCommandResult>
{
    private readonly GeocodingServices _geocodingServices;
    private readonly ForecastServices _forecastServices;
    private readonly PointServices _pointServices;
    public WeatherForecastCommandHandler(GeocodingServices geocodingServices, ForecastServices forecastServices, PointServices pointServices)
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

        return new WeatherForecastCommandResult
        {
            Forecast = forecastResponse.Properties.Periods.Select(
                period => new ForecastApi.Application.Forecast {
                    Number = period.Number,
                    Name = period.Name,
                    StartTime = period.StartTime,
                    EndTime = period.EndTime,
                    IsDaytime = period.IsDaytime,
                    Temperature = period.Temperature,
                    TemperatureUnit = period.TemperatureUnit,
                    TemperatureTrend = period.TemperatureTrend,
                    ProbabilityOfPrecipitation = new ProbabilityOfPrecipitation {
                        UnitCode = period.ProbabilityOfPrecipitation.UnitCode,
                        Value = period.ProbabilityOfPrecipitation.Value
                    },
                    Dewpoint = new Dewpoint {
                        UnitCode = period.Dewpoint.UnitCode,
                        Value = period.Dewpoint.Value
                    },
                    RelativeHumidity = new RelativeHumidity {
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

