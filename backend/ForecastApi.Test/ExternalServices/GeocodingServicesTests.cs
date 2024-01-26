using System.Net;
using System.Text.Json;
using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Geocodings.Models;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace ForecastApi.Test;

public class GeocodingServicesTests
{
    private readonly HttpClient _httpClient;
    private readonly IGeocodingServices _geocodingServices;
    private readonly IOptions<GeocodingConfiguration> _options;
    private readonly Mock<HttpMessageHandler> _handler = new();

    public GeocodingServicesTests()
    {
        _httpClient = new HttpClient(_handler.Object);
        _options = Options.Create(new GeocodingConfiguration
        {
            Benchmark = "Public_AR_Current",
            Format = "json",
            Vintage = "Current_Current"
        });
        _geocodingServices = new GeocodingServices(_httpClient, _options);
    }

    [Fact]
    public async Task Given_GetGeocoding_Then_Return_Geocoding()
    {
        // Arrange
        _httpClient.BaseAddress = new Uri("https://test.com/");
        _handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new Geocoding
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
                                        Y = -77                                    }
                                }
                            }
                        }
                }))
            });

        // Act
        var geocoding = await _geocodingServices.GetGeocodingAsync(new GeocodingRequest
        {
            Street = "1600 Pennsylvania Ave NW",
            City = "Washington",
            State = "DC",
            Zip = "20500"
        });

        // Assert
        Assert.NotNull(geocoding);
        Assert.Collection(geocoding.Result.AddressMatches,
            item =>
            {
                Assert.Equal(38, item.Coordinates.X);
                Assert.Equal(-77, item.Coordinates.Y);
            });
    }
}
