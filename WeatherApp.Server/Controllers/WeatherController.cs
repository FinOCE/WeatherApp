using WeatherApp.Server.Models;
using WeatherApp.Server.Services;

namespace WeatherApp.Server.Controllers;

[ApiController]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherController(
        ILogger<WeatherController> logger,
        IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("/forecast", Name = "GetForecast")]
    public async Task<IActionResult> GetForecast()
    {        
        string[] validTypes = {
            "daily",
            "hourly",
            "current-daily",
            "current-hourly"
        };
        
        // Validate query params
        IList<string> errors = new List<string>();

        string type = null!;
        try
        {
            if (!Request.Query.TryGetValue("type", out StringValues rawType))
                throw new IndexOutOfRangeException();
            
            type = rawType[0];

            if (!validTypes.Contains(type))
                errors.Add("The query param 'type' must be a valid type");
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'type' is required");
        }

        double latitude = 0;
        if (!Request.Query.TryGetValue("lat", out StringValues rawLatitude))
            errors.Add("The query param 'lat' is required");
        else if (!double.TryParse(rawLatitude[0], out latitude))
            errors.Add("The query param 'lat' must be a number");

        double longitude = 0;
        if (!Request.Query.TryGetValue("long", out StringValues rawLongitude))
            errors.Add("The query param 'long' is required");
        else if (!double.TryParse(rawLongitude[0], out longitude))
            errors.Add("The query param 'long' must be a number");

        string name = null!;
        try
        {
            if (!Request.Query.TryGetValue("name", out StringValues rawName))
                throw new IndexOutOfRangeException();
            
            name = rawName[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'name' is required");
        }
        
        string timezone = null!;
        try
        {
            if (!Request.Query.TryGetValue("tz", out StringValues rawTimezone))
                throw new IndexOutOfRangeException();
            
            timezone = rawTimezone[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'tz' is required");
        }

        if (errors.Count > 0)
            return new BadRequestObjectResult(errors.ToArray());

        Location location = new(latitude, longitude, name, timezone);

        // Get forecast depending on type
        try
        {
            if (type == "daily")
            {
                _logger.LogInformation(
                    "Successfully fetched daily forecast for location {name}",
                    name);

                DailyForecast[] res = await _weatherService.GetDailyForecastAsync(location);
                return new OkObjectResult(res);
            }
            else if (type == "hourly")
            {
                _logger.LogInformation(
                    "Successfully fetched hourly forecast for location {name}",
                    name);

                HourlyForecast[] res = await _weatherService.GetHourlyForecastAsync(location);
                return new OkObjectResult(res);
            }
            else if (type == "current-daily")
            {
                _logger.LogInformation(
                    "Successfully fetched current-daily forecast for location {name}",
                    name);

                DailyForecast res = await _weatherService.GetCurrentDayForecastAsync(location);
                return new OkObjectResult(res);
            }
            else if (type == "current-hourly")
            {
                _logger.LogInformation(
                    "Successfully fetched current-hourly forecast for location {name}",
                    name);

                HourlyForecast[] res = await _weatherService.GetCurrentDayHourlyForecastAsync(location);
                return new OkObjectResult(res);
            }
            else
            {
                _logger.LogError(
                    "Failed to fetch forecast with type {type}",
                    type);

                return new BadRequestObjectResult(
                    new string[] { "The query param 'type' must be a valid type" });
            }
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(
                new string[] { ex.Message });
        }
    }

    [HttpGet("/forecast/current", Name = "GetCurrent")]
    public async Task<IActionResult> GetCurrent()
    {
        // Validate query params
        IList<string> errors = new List<string>();

        double latitude = 0;
        if (!Request.Query.TryGetValue("lat", out StringValues rawLatitude))
            errors.Add("The query param 'lat' is required");
        else if (!double.TryParse(rawLatitude[0], out latitude))
            errors.Add("The query param 'lat' must be a number");

        double longitude = 0;
        if (!Request.Query.TryGetValue("long", out StringValues rawLongitude))
            errors.Add("The query param 'long' is required");
        else if (!double.TryParse(rawLongitude[0], out longitude))
            errors.Add("The query param 'long' must be a number");
        
        string name = null!;
        try
        {
            if (!Request.Query.TryGetValue("name", out StringValues rawName))
                throw new IndexOutOfRangeException();
            
            name = rawName[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'name' is required");
        }
        
        string timezone = null!;
        try
        {
            if (!Request.Query.TryGetValue("tz", out StringValues rawTimezone))
                throw new IndexOutOfRangeException();
            
            timezone = rawTimezone[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'tz' is required");
        }

        if (errors.Count > 0)
            return new BadRequestObjectResult(errors.ToArray());

        Location location = new(latitude, longitude, name, timezone);

        // Get current weather
        try
        {
            CurrentWeather res = await _weatherService.GetCurrentWeatherAsync(location);
            return new OkObjectResult(res);
        }
        catch (ArgumentException ex)
        {
            return new BadRequestObjectResult(
                new string[] { ex.Message });
        }
    }
}
