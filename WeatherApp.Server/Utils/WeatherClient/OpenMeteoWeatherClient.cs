using WeatherApp.Server.Models;

namespace WeatherApp.Server.Utils.WeatherClient;

/// <summary>
/// A weather client that fetches data from Open-Meteo.
/// </summary>
public class OpenMeteoWeatherClient : IWeatherClient
{
    private readonly HttpClient _httpClient;

    public OpenMeteoWeatherClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(OpenMeteoWeatherClient));
    }

    public async Task<CurrentWeather> GetCurrentWeatherAsync(Location location)
    {
        try
        {
            HttpResponseMessage res = await _httpClient.GetAsync(
                $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&current_weather=true&timezone={location.Timezone}");
            
            string json = await res.Content.ReadAsStringAsync();
            var weather = JsonSerializer.Deserialize<OpenMeteoCurrentWeatherResponse>(
                json);

            if (weather is null)
                throw new NullReferenceException();

            return new CurrentWeather(
                location,
                new(weather.CurrentWeather.WeatherCode),
                weather.CurrentWeather.Temperature,
                weather.CurrentWeather.WindSpeed,
                new(weather.CurrentWeather.WindDirection),
                weather.CurrentWeather.IsDay == 1,
                weather.CurrentWeather.Time);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            // Throw generic argument exception if anything goes wrong
            throw new ArgumentException(
                "Unable to get the current weather for the given location");
        }
    }
    
    public async Task<DailyForecast[]> GetDailyForecastAsync(Location location)
    {
        try
        {
            HttpResponseMessage res = await _httpClient.GetAsync(
                $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&daily=weathercode,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,sunrise,sunset,uv_index_max,precipitation_probability_max,windspeed_10m_max,winddirection_10m_dominant&timezone={location.Timezone}");

            string json = await res.Content.ReadAsStringAsync();
            var forecast =
                JsonSerializer.Deserialize<OpenMeteoDailyForecastResponse>(
                    json);

            if (forecast is null)
                throw new NullReferenceException();

            var forecasts = new DailyForecast[forecast.Daily.Time.Length];
            
            for (int i = 0; i < forecasts.Length; i++)
                forecasts[i] = new DailyForecast(
                    location,
                    new(forecast.Daily.WeatherCode[i]),
                    forecast.Daily.MaximumTemperature[i],
                    forecast.Daily.MinimumTemperature[i],
                    forecast.Daily.MaximumApparentTemperature[i],
                    forecast.Daily.MinimumApparentTemperature[i],
                    forecast.Daily.Sunrise[i],
                    forecast.Daily.Sunset[i],
                    forecast.Daily.UVIndex[i],
                    (double)forecast.Daily.PrecipitationProbability[i] / 100,
                    forecast.Daily.MaximumWindSpeed[i],
                    new(forecast.Daily.WindDirection[i]));
            
            return forecasts;
        }
        catch (Exception)
        {
            // Throw generic argument exception if anything goes wrong
            throw new ArgumentException(
                "Unable to get a forecast for the given location");
        }
    }

    public async Task<HourlyForecast[]> GetHourlyForecastAsync(Location location)
    {
        try
        {
            HttpResponseMessage res = await _httpClient.GetAsync(
                $"https://api.open-meteo.com/v1/forecast?latitude={location.Latitude}&longitude={location.Longitude}&hourly=temperature_2m,apparent_temperature,precipitation_probability,weathercode,visibility,uv_index,is_day,windspeed_1000hPa,winddirection_1000hPa&timezone={location.Timezone}&models=best_match");

            string json = await res.Content.ReadAsStringAsync();
            var forecast = JsonSerializer.Deserialize<OpenMeteoHourlyForecastResponse>(
                json);

            if (forecast is null)
                throw new NullReferenceException();

            var forecasts = new HourlyForecast[forecast.Hourly.Temperature.Length];

            for (int i = 0; i < forecasts.Length; i++)
            {
                forecasts[i] = new HourlyForecast(
                    location,
                    new(forecast.Hourly.WeatherCode[i]),
                    forecast.Hourly.Time[i],
                    forecast.Hourly.Temperature[i],
                    forecast.Hourly.ApparentTemperature[i],
                    (double)forecast.Hourly.PrecipitationProbability[i] / 100,
                    forecast.Hourly.Visibility[i] / 1000,
                    forecast.Hourly.WindSpeed[i],
                    new(forecast.Hourly.WindDirection[i]),
                    forecast.Hourly.UVIndex[i],
                    forecast.Hourly.IsDay[i] == 1);
            }

            return forecasts;
        }
        catch (Exception)
        {
            // Throw generic argument exception if anything goes wrong
            throw new ArgumentException(
                "Unable to get a forecast for the given location");
        }
    }

    public async Task<DailyForecast> GetCurrentDayForecastAsync(Location location)
    {
        return (await GetDailyForecastAsync(location))[0];
    }

    public async Task<HourlyForecast[]> GetCurrentDayHourlyForecastAsync(Location location)
    {
        return (await GetHourlyForecastAsync(location))[0..24];
    }
}

public class OpenMeteoCurrentWeatherResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = null!;

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; } = null!;

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("current_weather")]
    public OpenMeteoCurrentWeather CurrentWeather { get; set; } = null!;
}

public class OpenMeteoDailyForecastResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }
    
    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }
    
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = null!;
    
    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; } = null!;

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("daily_units")]
    public OpenMeteoDailyUnits DailyUnits { get; set; } = null!;

    [JsonPropertyName("daily")]
    public OpenMeteoDaily Daily { get; set; } = null!;
}

public class OpenMeteoHourlyForecastResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("generationtime_ms")]
    public double GenerationTimeMs { get; set; }

    [JsonPropertyName("utc_offset_seconds")]
    public int UtcOffsetSeconds { get; set; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = null!;

    [JsonPropertyName("timezone_abbreviation")]
    public string TimezoneAbbreviation { get; set; } = null!;

    [JsonPropertyName("elevation")]
    public double Elevation { get; set; }

    [JsonPropertyName("hourly_units")]
    public OpenMeteoHourlyUnits HourlyUnits { get; set; } = null!;

    [JsonPropertyName("hourly")]
    public OpenMeteoHourly Hourly { get; set; } = null!;
}

public class OpenMeteoCurrentWeather
{
    [JsonPropertyName("temperature")]
    public double Temperature { get; set; }

    [JsonPropertyName("windspeed")]
    public double WindSpeed { get; set; }

    [JsonPropertyName("winddirection")]
    public double WindDirection { get; set; }

    [JsonPropertyName("weathercode")]
    public int WeatherCode { get; set; }

    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
}

public class OpenMeteoDailyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = null!;

    [JsonPropertyName("weathercode")]
    public string WeatherCode { get; set; } = null!;

    [JsonPropertyName("temperature_2m_max")]
    public string MaximumTemperature { get; set; } = null!;

    [JsonPropertyName("temperature_2m_min")]
    public string MinimumTemperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature_max")]
    public string MaximumApparentTemperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature_min")]
    public string MinimumApparentTemperature { get; set; } = null!;

    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; } = null!;

    [JsonPropertyName("sunset")]
    public string Sunset { get; set; } = null!;

    [JsonPropertyName("uv_index_max")]
    public string UVIndex { get; set; } = null!;

    [JsonPropertyName("precipitation_probability_max")]
    public string PrecipitationProbability { get; set; } = null!;

    [JsonPropertyName("windspeed_10m_max")]
    public string MaximumWindSpeed { get; set; } = null!;

    [JsonPropertyName("winddirection_10m_dominant")]
    public string WindDirection { get; set; } = null!;
}

public class OpenMeteoDaily
{
    [JsonPropertyName("time")]
    public DateTime[] Time { get; set; } = null!;

    [JsonPropertyName("weathercode")]
    public int[] WeatherCode { get; set; } = null!;

    [JsonPropertyName("temperature_2m_max")]
    public double[] MaximumTemperature { get; set; } = null!;

    [JsonPropertyName("temperature_2m_min")]
    public double[] MinimumTemperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature_max")]
    public double[] MaximumApparentTemperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature_min")]
    public double[] MinimumApparentTemperature { get; set; } = null!;

    [JsonPropertyName("sunrise")]
    public DateTime[] Sunrise { get; set; } = null!;

    [JsonPropertyName("sunset")]
    public DateTime[] Sunset { get; set; } = null!;

    [JsonPropertyName("uv_index_max")]
    public double[] UVIndex { get; set; } = null!;

    [JsonPropertyName("precipitation_probability_max")]
    public int[] PrecipitationProbability { get; set; } = null!;

    [JsonPropertyName("windspeed_10m_max")]
    public double[] MaximumWindSpeed { get; set; } = null!;

    [JsonPropertyName("winddirection_10m_dominant")]
    public int[] WindDirection { get; set; } = null!;
}

public class OpenMeteoHourlyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; } = null!;

    [JsonPropertyName("temperature_2m")]
    public string Temperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature")]
    public string ApparentTemperature { get; set; } = null!;

    [JsonPropertyName("precipitation_probability")]
    public string PrecipitationProbability { get; set; } = null!;

    [JsonPropertyName("weathercode")]
    public string WeatherCode { get; set; } = null!;

    [JsonPropertyName("visibility")]
    public string Visibility { get; set; } = null!;

    [JsonPropertyName("windspeed_1000hPa")]
    public string WindSpeed { get; set; } = null!;

    [JsonPropertyName("winddirection_1000hPa")]
    public string WindDirection { get; set; } = null!;

    [JsonPropertyName("uv_index")]
    public string UVIndex { get; set; } = null!;
    
    [JsonPropertyName("is_day")]
    public string IsDay { get; set; } = null!;
}

public class OpenMeteoHourly
{
    [JsonPropertyName("time")]
    public DateTime[] Time { get; set; } = null!;

    [JsonPropertyName("temperature_2m")]
    public double[] Temperature { get; set; } = null!;

    [JsonPropertyName("apparent_temperature")]
    public double[] ApparentTemperature { get; set; } = null!;

    [JsonPropertyName("precipitation_probability")]
    public int[] PrecipitationProbability { get; set; } = null!;

    [JsonPropertyName("weathercode")]
    public int[] WeatherCode { get; set; } = null!;

    [JsonPropertyName("visibility")]
    public double[] Visibility { get; set; } = null!;

    [JsonPropertyName("windspeed_1000hPa")]
    public double[] WindSpeed { get; set; } = null!;

    [JsonPropertyName("winddirection_1000hPa")]
    public int[] WindDirection { get; set; } = null!;

    [JsonPropertyName("uv_index")]
    public double[] UVIndex { get; set; } = null!;

    [JsonPropertyName("is_day")]
    public int[] IsDay { get; set; } = null!;
}
