namespace WeatherApp.API.Models;

/// <summary>
/// The current weather for a given location.
/// </summary>
public class CurrentWeather
{
    public Location Location { get; set; }

    public Weather Weather { get; set; }
    
    public double Temperature { get; set; }

    public double WindSpeed { get; set; }

    public Cardinal WindDirection { get; set; }

    public bool IsDay { get; set; }

    public DateTime Time { get; set; } 

    public CurrentWeather(
        Location location,
        Weather weather,
        double temperature,
        double windSpeed,
        Cardinal windDirection,
        bool isDay,
        DateTime time)
    {
        Location = location;
        Weather = weather;
        Temperature = temperature;
        WindSpeed = windSpeed;
        WindDirection = windDirection;
        IsDay = isDay;
        Time = time;
    }
}
