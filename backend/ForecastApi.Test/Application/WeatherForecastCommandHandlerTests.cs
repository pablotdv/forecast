using System.Net;
using System.Text.Json;
using ForecastApi.Application;
using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Geocodings.Models;
using ForecastApi.ExternalServices.Weathers;
using ForecastApi.ExternalServices.Weathers.Models;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace ForecastApi.Test;

public class WeatherForecastCommandHandlerTests
{
    private readonly WeatherForecastCommandHandler _handler;
    private readonly Mock<IGeocodingServices> _geocodingServices = new();
    private readonly Mock<IForecastServices> _forecastServices = new();
    private readonly Mock<IPointServices> _pointServices = new();

    public WeatherForecastCommandHandlerTests()
    {
        _handler = new WeatherForecastCommandHandler(_geocodingServices.Object, _forecastServices.Object, _pointServices.Object);
    }

    [Fact]
    public async Task Given_Geocoding_Handle_Then_Return_Forecast()
    {
        // Arrange
        _geocodingServices.Setup(x => x.GetGeocodingAsync(It.IsAny<GeocodingRequest>()))
            .ReturnsAsync(new Geocoding
            {
                Result =
                    new Result
                    {
                        AddressMatches = new List<AddressMatch>
                        {
                            new AddressMatch
                            {
                                Coordinates = new Coordinates
                                {
                                    X = 38,
                                    Y = -77
                                }
                            }
                        }
                    }
            });
        _forecastServices.Setup(x => x.GetForecastAsync(It.IsAny<ForecastRequest>())).ReturnsAsync(new Forecast
        {
            Properties = new Properties
            {
                Periods = new List<Period>
                    {
                        new Period
                        {
                            Number = 1,
                            Name = "Today",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now.AddDays(1),
                            IsDaytime = true,
                            Temperature = 50,
                            TemperatureUnit = "F",
                            TemperatureTrend = "Falling",
                            WindSpeed = "5 mph",
                            WindDirection = "N",
                            Icon = "https://test.com",
                            ShortForecast = "Sunny",
                            DetailedForecast = "Sunny",
                            Dewpoint = new Dewpoint
                            {
                                UnitCode = "F",
                                Value = 50
                            },
                            ProbabilityOfPrecipitation = new ProbabilityOfPrecipitation
                            {
                                UnitCode = "F",
                                Value = 50
                            },
                            RelativeHumidity = new RelativeHumidity
                            {
                                UnitCode = "F",
                                Value = 50
                            }
                        }
                    }
            }
        });

        _pointServices.Setup(x => x.GetPointAsync(It.IsAny<PointRequest>())).ReturnsAsync(new Point
        {
            Properties = new ForecastApi.ExternalServices.Weathers.Models.PointProperties
            {
                GridId = "Test",
                GridX = 1,
                GridY = 1
            }
        });

        // Act
        var result = await _handler.Handle(new WeatherForecastCommand
        {
            Street = "123 Main St",
            City = "Test",
            State = "VA",
            Zip = "12345"
        }, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Forecast);
        Assert.Collection(result.Forecast,
            item =>
            {
                Assert.Equal(1, item.Number);
                Assert.Equal("Today", item.Name);
                Assert.True(item.IsDaytime);
                Assert.Equal(50, item.Temperature);
                Assert.Equal("F", item.TemperatureUnit);
                Assert.Equal("Falling", item.TemperatureTrend);
                Assert.Equal("5 mph", item.WindSpeed);
                Assert.Equal("N", item.WindDirection);
                Assert.Equal("https://test.com", item.Icon);
                Assert.Equal("Sunny", item.ShortForecast);
                Assert.Equal("Sunny", item.DetailedForecast);
            });
    }
}
