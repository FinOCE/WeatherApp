using WeatherApp.Server.Models;
using WeatherApp.Server.Services;

namespace WeatherApp.Server.Controllers;

[ApiController]
[Route("/weather")]
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

    [HttpGet(Name = "GetForecast")]
    public async Task<IActionResult> GetForecast()
    {        
        string[] validTypes = new string[]
        {
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

        if (!Request.Query.TryGetValue("name", out StringValues rawName))
            errors.Add("The query param 'name' is required");

        string name = null!;
        try
        {
            name = rawName[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'type' is required");
        }

        if (errors.Count > 0)
            return new BadRequestObjectResult(errors.ToArray());

        Location location = new(latitude, longitude, name);

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
        catch (ArgumentException)
        {
            return new BadRequestObjectResult(
                new string[]
                {
                    "Unable to find weather data for the given location"
                });
        }
    }

    [HttpGet(Name = "GetCurrent")]
    [Route("/current")]
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

        if (!Request.Query.TryGetValue("name", out StringValues rawName))
            errors.Add("The query param 'name' is required");

        string name = null!;
        try
        {
            name = rawName[0];
        }
        catch (IndexOutOfRangeException)
        {
            errors.Add("The query param 'type' is required");
        }

        if (errors.Count > 0)
            return new BadRequestObjectResult(errors.ToArray());

        Location location = new(latitude, longitude, name);

        // Get current weather
        try
        {
            CurrentWeather res = await _weatherService.GetCurrentWeatherAsync(location);
            return new OkObjectResult(res);
        }
        catch (ArgumentException)
        {
            return new BadRequestObjectResult(
                new string[]
                {
                    "Unable to find weather data for the given location"
                });
        }
    }
}
