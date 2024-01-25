using ForecastApi.Application;
using ForecastApi.ExternalServices;
using ForecastApi.ExternalServices.Geocodings;
using ForecastApi.ExternalServices.Weathers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(WeatherForecastCommandHandler).Assembly));


builder.Services.AddHttpClient("WeatherService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Weather:BaseAddress"));
    client.DefaultRequestHeaders.Add("User-Agent", "(https://pablotdv.azurewebsites.net,pablotdvsm@gmail.com)");
});
builder.Services.AddTransient<PointServices>();
builder.Services.AddTransient<ForecastServices>();

builder.Services.AddHttpClient<GeocodingServices>(
    client =>
    {
        client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Geocoding:BaseAddress"));                
    }
);
builder.Services.Configure<GeocodingConfiguration>(
    builder.Configuration.GetSection("Geocoding:Configuration")
);

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAnyOrigin", builder =>
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod());
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
