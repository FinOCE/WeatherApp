namespace WeatherApp.Server.Models;

/// <summary>
/// The hourly weather forecast for a given location.
/// </summary>
public class HourlyForecast
{
    public Location Location { get; set; }

    public Weather Weather { get; set; }

    public DateTime Time { get; set; }

    public double Temperature { get; set; }

    public double ApparentTemperature { get; set; }

    public double PrecipitationProbability { get; set; }

    public double Visibility { get; set; }

    public double WindSpeed { get; set; }

    public Cardinal WindDirection { get; set; }

    public double UVIndex { get; set; }

    public bool IsDay { get; set; }

    public HourlyForecast(
        Location location,
        Weather weather,
        DateTime time,
        double temperature,
        double apparentTemperature,
        double precipitationProbability,
        double visibility,
        double windSpeed,
        Cardinal windDirection,
        double uVIndex,
        bool isDay)
    {
        Location = location;
        Weather = weather;
        Time = time;
        Temperature = temperature;
        ApparentTemperature = apparentTemperature;
        PrecipitationProbability = precipitationProbability;
        Visibility = visibility;
        WindSpeed = windSpeed;
        WindDirection = windDirection;
        UVIndex = uVIndex;
        IsDay = isDay;
    }
}
