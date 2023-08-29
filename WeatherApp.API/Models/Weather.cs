namespace WeatherApp.API.Models;

/// <summary>
/// Represents the current weather.
/// </summary>
public class Weather
{
    /// <summary>
    /// The WMO Weather interpretation code.
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// The label for the weather based on the WMO code as defined 
    /// <see href="https://www.nodc.noaa.gov/archive/arc0021/0002199/1.1/data/0-data/HTML/WMO-CODE/WMO4677.HTM">
    /// here</see>.
    /// <br />
    /// <br />
    /// <b>Currently only contains values used by Open-Meteo, so this does not 
    /// implement an exhaustive list of all WMO codes. If a new weather client
    /// is to be used, this must be added.</b>
    /// </summary>
    public string Description
    { 
        get
        {
            return Code switch
            {
                0 => "Clear sky",
                1 => "Mainly clear",
                2 => "Partly cloudy",
                3 => "Overcast",
                45 => "Fog",
                48 => "Depositing rime fog",
                51 => "Light drizzle",
                53 => "Moderate drizzle",
                55 => "Dense drizzle",
                56 => "Freezing light drizzle",
                57 => "Freezing dense drizzle",
                61 => "Slight rain",
                63 => "Moderate rain",
                65 => "Heavy rain",
                66 => "Freezing light rain",
                67 => "Freezing heavy rain",
                71 => "Slight snow fall",
                73 => "Moderate snow fall",
                75 => "Heavy snow fall",
                77 => "Snow grains",
                80 => "Slight rain showers",
                81 => "Moderate rain showers",
                82 => "Violent rain showers",
                85 => "Slight snow showers",
                86 => "Heavy snow showers",
                95 => "Slight to moderate thunderstorms",
                96 => "Thunderstorm with slight hail",
                99 => "Thunderstorm with heavy hail",
                _ => throw new ArgumentException()
            };
        }
    }

    public Weather(int weatherCode)
    {
        Code = weatherCode;
    }
}
