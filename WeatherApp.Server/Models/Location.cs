namespace WeatherApp.Server.Models;

/// <summary>
/// A location on Earth, represented by latitude and longitude.
/// </summary>
public class Location
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public string Name { get; set; }
    
    public string Timezone { get; set; }

    public Location(
        double latitude,
        double longitude,
        string name,
        string timezone)
    {
        Latitude = latitude;
        Longitude = longitude;
        Name = name;
        Timezone = timezone;
    }
}
