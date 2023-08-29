namespace WeatherApp.API.Models;

/// <summary>
/// Represents a cardinal direction.
/// </summary>
public class Cardinal
{
    /// <summary>
    /// The angle of the direction, where north is 0 going clockwise.
    /// </summary>
    public double Angle { get; set; }

    /// <summary>
    /// The label for the 16 segment cardinal direction. Each direction is 12.25 
    /// degrees in either direction of the exact point. For example, east is at 
    /// 90 degrees, but is displayed for values in [78.75, 101.25).
    /// </summary>
    public string Direction
    {
        get
        {
            return Angle switch
            {
                var x when x >= 0 && x < 11.25 => "North",
                var x when x >= 11.25 && x < 33.75 => "North-Northeast",
                var x when x >= 33.75 && x < 56.25 => "Northeast",
                var x when x >= 56.25 && x < 78.75 => "East-Northeast",
                var x when x >= 78.75 && x < 101.25 => "East",
                var x when x >= 101.25 && x < 123.75 => "East-Southeast",
                var x when x >= 123.75 && x < 146.25 => "Southeast",
                var x when x >= 146.25 && x < 168.75 => "South-Southeast",
                var x when x >= 168.75 && x < 191.25 => "South",
                var x when x >= 191.25 && x < 213.75 => "South-Southwest",
                var x when x >= 213.75 && x < 236.25 => "Southwest",
                var x when x >= 236.25 && x < 258.75 => "West-Southwest",
                var x when x >= 258.75 && x < 281.25 => "West",
                var x when x >= 281.25 && x < 303.75 => "West-Northwest",
                var x when x >= 303.75 && x < 326.25 => "Northwest",
                var x when x >= 326.25 && x < 348.75 => "North-Northwest",
                var x when x >= 348.75 && x <= 360 => "North",
                _ => throw new ArgumentOutOfRangeException()
                // Thank you copilot for prefilling!
            };
        }
    }

    /// <summary>
    /// The label for the cardinal direction's initials. Each direction is 12.25 
    /// degrees in either direction of the exact point. For example, E is at 90 
    /// degrees, but is displayed for values in [78.75, 101.25).
    /// </summary>
    public string DirectionInitials
    {
        get
        {
            return Angle switch
            {
                var x when x >= 0 && x < 11.25 => "N",
                var x when x >= 11.25 && x < 33.75 => "NNE",
                var x when x >= 33.75 && x < 56.25 => "NE",
                var x when x >= 56.25 && x < 78.75 => "ENE",
                var x when x >= 78.75 && x < 101.25 => "E",
                var x when x >= 101.25 && x < 123.75 => "ESE",
                var x when x >= 123.75 && x < 146.25 => "SE",
                var x when x >= 146.25 && x < 168.75 => "SSE",
                var x when x >= 168.75 && x < 191.25 => "S",
                var x when x >= 191.25 && x < 213.75 => "SSW",
                var x when x >= 213.75 && x < 236.25 => "SW",
                var x when x >= 236.25 && x < 258.75 => "WSW",
                var x when x >= 258.75 && x < 281.25 => "W",
                var x when x >= 281.25 && x < 303.75 => "WNW",
                var x when x >= 303.75 && x < 326.25 => "NW",
                var x when x >= 326.25 && x < 348.75 => "NNW",
                var x when x >= 348.75 && x <= 360 => "N",
                _ => throw new ArgumentOutOfRangeException()
                // Thank you copilot for prefilling!
            };
        }
    }

    public Cardinal(double angle)
    {
        Angle = angle;
    }
}
