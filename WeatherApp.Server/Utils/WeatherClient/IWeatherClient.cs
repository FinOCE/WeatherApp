using WeatherApp.Server.Models;

namespace WeatherApp.Server.Utils.WeatherClient;

/// <summary>
/// A client to request weather data from.
/// </summary>
public interface IWeatherClient
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
    Task<DailyForecast> GetCurrentDayForecastAsync(Location location);

    /// <summary>
    /// Get the weather forecast at the given location for the current day with 
    /// hourly increments.
    /// </summary>
    /// <param name="location">The location to get forecast data for</param>
    /// <returns>The current day's hourly forecast for the given location</returns>
    Task<HourlyForecast[]> GetCurrentDayHourlyForecastAsync(Location location);
}
