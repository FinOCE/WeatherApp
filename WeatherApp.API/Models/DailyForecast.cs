namespace WeatherApp.API.Models;

/// <summary>
/// The daily weather forecast for a given location.
/// </summary>
public class DailyForecast
{
    public Location Location { get; set; }

    public Weather Weather { get; set; }

    public double MaximumTemperature { get; set; }

    public double MinimumTemperature { get; set; }

    public double MaximumApparentTemperature { get; set; }

    public double MinimumApparentTemperature { get; set; }

    public DateTime Sunrise { get; set; }

    public DateTime Sunset { get; set; }

    public double UVIndex { get; set; }

    public double PrecipitationProbability { get; set; }

    public double MaximumWindSpeed { get; set; }

    public Cardinal WindDirection { get; set; }

    public DailyForecast(
        Location location,
        Weather weather,
        double maximumTemperature,
        double minimumTemperature,
        double maximumApparentTemperature,
        double minimumApparentTemperature,
        DateTime sunrise,
        DateTime sunset,
        double uvIndex,
        double precipitationProbability,
        double maximumWindSpeed,
        Cardinal windDirection)
    {
        Location = location;
        Weather = weather;
        MaximumTemperature = maximumTemperature;
        MinimumTemperature = minimumTemperature;
        MaximumApparentTemperature = maximumApparentTemperature;
        MinimumApparentTemperature = minimumApparentTemperature;
        Sunrise = sunrise;
        Sunset = sunset;
        UVIndex = uvIndex;
        PrecipitationProbability = precipitationProbability;
        MaximumWindSpeed = maximumWindSpeed;
        WindDirection = windDirection;
    }
}
