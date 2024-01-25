using MediatR;

namespace ForecastApi.Application;

public class WeatherForecastCommand : IRequest<WeatherForecastCommandResult>
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }

}

