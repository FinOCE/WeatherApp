using WeatherApp.Server.Models;
using WeatherApp.Server.Utils.WeatherClient;

namespace WeatherApp.Server.Services;

public interface IWeatherService
{
    /// <summary>
    /// Get the current weather at the given location.
    /// </summary>
    /// <param name="location">The location to get weather data for</param>
    /// <returns>The current weather at the given location</returns>
    Task<CurrentWeather> GetCurrentWeatherAsync(Location location);

    /// <summary>
    /// Get the weather forecast at the given location with daily increments.
    /// </summary>
    /// <param name="location">The location to get forecast data for</param>
    /// <returns>The daily forecast for the given location</returns>
    Task<DailyForecast[]> GetDailyForecastAsync(Location location);

    /// <summary>
    /// Get the weather forecast at the given location with hourly increments.
    /// </summary>
    /// <param name="location">The location to get forecast data for</param>
    /// <returns>The hourly forecast for the given location</returns>
    Task<HourlyForecast[]> GetHourlyForecastAsync(Location location);

    /// <summary>
    /// Get the weather forecast at the given location for the current day.
    /// </summary>
    /// <param name="location">The location to get forecast data for</param>
    /// <returns>The current day's forecast for the given location</returns>
    Task<DailyForecast> GetCurrentDayForecast(Location location);

    /// <summary>
    /// Get the weather forecast at the given location for the current day with 
    /// hourly increments.
    /// </summary>
    /// <param name="location">The location to get forecast data for</param>
    /// <returns>The current day's hourly forecast for the given location</returns>
    Task<HourlyForecast[]> GetCurrentDayHourlyForecast(Location location);
}

public class WeatherService : IWeatherService
{
    private readonly IWeatherClient _weatherClient;

    public WeatherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public async Task<CurrentWeather> GetCurrentWeatherAsync(Location location)
    {
        return await _weatherClient.GetCurrentWeatherAsync(location);
    }

    public async Task<DailyForecast[]> GetDailyForecastAsync(Location location)
    {
        return await _weatherClient.GetDailyForecastAsync(location);
    }

    public async Task<HourlyForecast[]> GetHourlyForecastAsync(Location location)
    {
        return await _weatherClient.GetHourlyForecastAsync(location);
    }

    public async Task<DailyForecast> GetCurrentDayForecast(Location location)
    {
        return await _weatherClient.GetCurrentDayForecast(location);
    }

    public async Task<HourlyForecast[]> GetCurrentDayHourlyForecast(Location location)
    {
        return await _weatherClient.GetCurrentDayHourlyForecast(location);
    }

    // In the current state you could just use the client directly, but this
    // allows for more flexibility in the future and follows better practices.
}
