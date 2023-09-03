using WeatherApp.Server.Mocks;
using WeatherApp.Server.Models;

namespace WeatherApp.Server.Utils.WeatherClient;

[TestClass]
public class OpenMeteoWeatherClientTests
{
    [TestMethod]
    public async Task GetCurrentWeatherAsync_GetsCurrentWeather()
    {
        // Arrange
        double windSpeed = 10;
        double windDirection = 60;
        
        Location location = new(0, 0, "Name", "AEST");

        OpenMeteoCurrentWeatherResponse weatherResponse = new()
        {
            Latitude = 0,
            Longitude = 0,
            GenerationTimeMs = 0,
            UtcOffsetSeconds = 0,
            Timezone = "Australia/Brisbane",
            TimezoneAbbreviation = "AEST",
            Elevation = 0,
            CurrentWeather = new()
            {
                Temperature = 0,
                WindSpeed = windSpeed,
                WindDirection = windDirection,
                WeatherCode = 0,
                IsDay = 0,
                Time = DateTime.Now
            }
        };
        
        MockHttpMessageHandler mockHttpMessageHandler = new(
            JsonSerializer.Serialize(weatherResponse),
            HttpStatusCode.OK);
        
        HttpClient httpClient = new(mockHttpMessageHandler);
        IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        OpenMeteoWeatherClient weatherClient = new(httpClientFactory);

        // Act
        CurrentWeather res = await weatherClient.GetCurrentWeatherAsync(location);

        // Assert
        Assert.AreEqual(windSpeed, res.WindSpeed);
        Assert.AreEqual(windDirection, res.WindDirection.Angle);
    }

    [TestMethod]
    public async Task GetDailyForecastAsync_GetsDailyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Name", "AEST");

        DateTime[] time = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        int[] weatherCode = new int[7].Select(_ => 45).ToArray();
        double[] maximumTemperature = new double[7].Select(_ => 10d).ToArray();
        double[] minimumTemperature = new double[7].Select(_ => 0d).ToArray();
        double[] maximumApparentTemperature = new double[7].Select(_ => 0d).ToArray();
        double[] minimumApparentTemperature = new double[7].Select(_ => 0d).ToArray();
        DateTime[] sunrise = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        DateTime[] sunset = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        double[] uvIndex = new double[7].Select(_ => 0d).ToArray();
        int[] precipitationProbability = new int[7].Select(_ => 0).ToArray();
        double[] maximumWindSpeed = new double[7].Select(_ => 0d).ToArray();
        int[] windDirection = new int[7].Select(_ => 0).ToArray();

        OpenMeteoDailyForecastResponse forecastResponse = new()
        {
            Latitude = 0,
            Longitude = 0,
            GenerationTimeMs = 0,
            UtcOffsetSeconds = 0,
            Timezone = "Australia/Brisbane",
            TimezoneAbbreviation = "AEST",
            Elevation = 0,
            Daily = new()
            {
                Time = time,
                WeatherCode = weatherCode,
                MaximumTemperature = maximumTemperature,
                MinimumTemperature = minimumTemperature,
                MaximumApparentTemperature = maximumApparentTemperature,
                MinimumApparentTemperature = minimumApparentTemperature,
                Sunrise = sunrise,
                Sunset = sunset,
                UVIndex = uvIndex,
                PrecipitationProbability = precipitationProbability,
                MaximumWindSpeed = maximumWindSpeed,
                WindDirection = windDirection
            },
            DailyUnits = new()
            {
                Time = "",
                WeatherCode = "",
                MaximumTemperature = "",
                MinimumTemperature = "",
                MaximumApparentTemperature = "",
                MinimumApparentTemperature = "",
                Sunrise = "",
                Sunset = "",
                UVIndex = "",
                PrecipitationProbability = "",
                MaximumWindSpeed = "",
                WindDirection = ""
            }
        };

        MockHttpMessageHandler mockHttpMessageHandler = new(
            JsonSerializer.Serialize(forecastResponse),
            HttpStatusCode.OK);

        HttpClient httpClient = new(mockHttpMessageHandler);
        IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        OpenMeteoWeatherClient weatherClient = new(httpClientFactory);

        // Act
        DailyForecast[] res = await weatherClient.GetDailyForecastAsync(location);

        // Assert
        Assert.AreEqual(7, res.Length);
        Assert.AreEqual(10d, res[0].MaximumTemperature);
        Assert.AreEqual(45, res[0].Weather.Code);
    }

    [TestMethod]
    public async Task GetHourlyForecastAsync_GetsHourlyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Name", "AEST");

        DateTime[] time = new DateTime[168].Select(_ => DateTime.Now).ToArray();
        double[] temperature = new double[168].Select(_ => 10d).ToArray();
        double[] apparentTemperature = new double[168].Select(_ => 0d).ToArray();
        int[] precipitationProbability = new int[168].Select(_ => 0).ToArray();
        int[] weatherCode = new int[168].Select(_ => 45).ToArray();
        double[] visibility = new int[168].Select(_ => 0d).ToArray();
        double[] windSpeed = new double[168].Select(_ => 0d).ToArray();
        int[] windDirection = new int[168].Select(_ => 0).ToArray();
        double[] uvIndex = new double[168].Select(_ => 0d).ToArray();
        int[] isDay = new int[168].Select(_ => 0).ToArray();

        OpenMeteoHourlyForecastResponse forecastResponse = new()
        {
            Latitude = 0,
            Longitude = 0,
            GenerationTimeMs = 0,
            UtcOffsetSeconds = 0,
            Timezone = "Australia/Brisbane",
            TimezoneAbbreviation = "AEST",
            Elevation = 0,
            Hourly = new()
            {
                Time = time,
                Temperature = temperature,
                ApparentTemperature = apparentTemperature,
                PrecipitationProbability = precipitationProbability,
                WeatherCode = weatherCode,
                Visibility = visibility,
                WindSpeed = windSpeed,
                WindDirection = windDirection,
                UVIndex = uvIndex,
                IsDay = isDay
            },
            HourlyUnits = new()
            {
                Time = "",
                Temperature = "",
                ApparentTemperature = "",
                PrecipitationProbability = "",
                WeatherCode = "",
                Visibility = "",
                WindSpeed = "",
                WindDirection = "",
                UVIndex = "",
                IsDay = ""
            }
        };
        
        MockHttpMessageHandler mockHttpMessageHandler = new(
            JsonSerializer.Serialize(forecastResponse),
            HttpStatusCode.OK);

        HttpClient httpClient = new(mockHttpMessageHandler);
        IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        OpenMeteoWeatherClient weatherClient = new(httpClientFactory);

        // Act
        HourlyForecast[] res = await weatherClient.GetHourlyForecastAsync(location);

        // Assert
        Assert.AreEqual(168, res.Length);
        Assert.AreEqual(10d, res[0].Temperature);
        Assert.AreEqual(45, res[0].Weather.Code);
    }

    [TestMethod]
    public async Task GetCurrentDayForecastAsync_GetsCurrentDayForecast()
    {
        // Arrange
        Location location = new(0, 0, "Name", "AEST");

        DateTime[] time = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        int[] weatherCode = new int[7].Select(_ => 45).ToArray();
        double[] maximumTemperature = new double[7].Select(_ => 10d).ToArray();
        double[] minimumTemperature = new double[7].Select(_ => 0d).ToArray();
        double[] maximumApparentTemperature = new double[7].Select(_ => 0d).ToArray();
        double[] minimumApparentTemperature = new double[7].Select(_ => 0d).ToArray();
        DateTime[] sunrise = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        DateTime[] sunset = new DateTime[7].Select(_ => DateTime.Now).ToArray();
        double[] uvIndex = new double[7].Select(_ => 0d).ToArray();
        int[] precipitationProbability = new int[7].Select(_ => 0).ToArray();
        double[] maximumWindSpeed = new double[7].Select(_ => 0d).ToArray();
        int[] windDirection = new int[7].Select(_ => 0).ToArray();

        OpenMeteoDailyForecastResponse forecastResponse = new()
        {
            Latitude = 0,
            Longitude = 0,
            GenerationTimeMs = 0,
            UtcOffsetSeconds = 0,
            Timezone = "Australia/Brisbane",
            TimezoneAbbreviation = "AEST",
            Elevation = 0,
            Daily = new()
            {
                Time = time,
                WeatherCode = weatherCode,
                MaximumTemperature = maximumTemperature,
                MinimumTemperature = minimumTemperature,
                MaximumApparentTemperature = maximumApparentTemperature,
                MinimumApparentTemperature = minimumApparentTemperature,
                Sunrise = sunrise,
                Sunset = sunset,
                UVIndex = uvIndex,
                PrecipitationProbability = precipitationProbability,
                MaximumWindSpeed = maximumWindSpeed,
                WindDirection = windDirection
            },
            DailyUnits = new()
            {
                Time = "",
                WeatherCode = "",
                MaximumTemperature = "",
                MinimumTemperature = "",
                MaximumApparentTemperature = "",
                MinimumApparentTemperature = "",
                Sunrise = "",
                Sunset = "",
                UVIndex = "",
                PrecipitationProbability = "",
                MaximumWindSpeed = "",
                WindDirection = ""
            }
        };

        MockHttpMessageHandler mockHttpMessageHandler = new(
            JsonSerializer.Serialize(forecastResponse),
            HttpStatusCode.OK);

        HttpClient httpClient = new(mockHttpMessageHandler);
        IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        OpenMeteoWeatherClient weatherClient = new(httpClientFactory);

        // Act
        DailyForecast res = await weatherClient.GetCurrentDayForecastAsync(location);

        // Assert
        Assert.AreEqual(10d, res.MaximumTemperature);
        Assert.AreEqual(45, res.Weather.Code);
    }

    [TestMethod]
    public async Task GetCurrentDayHourlyForecastAsync_GetsCurrentDayHourlyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Name", "AEST");

        DateTime[] time = new DateTime[168].Select(_ => DateTime.Now).ToArray();
        double[] temperature = new double[168].Select(_ => 10d).ToArray();
        double[] apparentTemperature = new double[168].Select(_ => 0d).ToArray();
        int[] precipitationProbability = new int[168].Select(_ => 0).ToArray();
        int[] weatherCode = new int[168].Select(_ => 45).ToArray();
        double[] visibility = new int[168].Select(_ => 0d).ToArray();
        double[] windSpeed = new double[168].Select(_ => 0d).ToArray();
        int[] windDirection = new int[168].Select(_ => 0).ToArray();
        double[] uvIndex = new double[168].Select(_ => 0d).ToArray();
        int[] isDay = new int[168].Select(_ => 0).ToArray();

        OpenMeteoHourlyForecastResponse forecastResponse = new()
        {
            Latitude = 0,
            Longitude = 0,
            GenerationTimeMs = 0,
            UtcOffsetSeconds = 0,
            Timezone = "Australia/Brisbane",
            TimezoneAbbreviation = "AEST",
            Elevation = 0,
            Hourly = new()
            {
                Time = time,
                Temperature = temperature,
                ApparentTemperature = apparentTemperature,
                PrecipitationProbability = precipitationProbability,
                WeatherCode = weatherCode,
                Visibility = visibility,
                WindSpeed = windSpeed,
                WindDirection = windDirection,
                UVIndex = uvIndex,
                IsDay = isDay
            },
            HourlyUnits = new()
            {
                Time = "",
                Temperature = "",
                ApparentTemperature = "",
                PrecipitationProbability = "",
                WeatherCode = "",
                Visibility = "",
                WindSpeed = "",
                WindDirection = "",
                UVIndex = "",
                IsDay = ""
            }
        };

        MockHttpMessageHandler mockHttpMessageHandler = new(
            JsonSerializer.Serialize(forecastResponse),
            HttpStatusCode.OK);

        HttpClient httpClient = new(mockHttpMessageHandler);
        IHttpClientFactory httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

        OpenMeteoWeatherClient weatherClient = new(httpClientFactory);

        // Act
        HourlyForecast[] res = await weatherClient.GetCurrentDayHourlyForecastAsync(location);

        // Assert
        Assert.AreEqual(24, res.Length);
        Assert.AreEqual(10d, res[0].Temperature);
        Assert.AreEqual(45, res[0].Weather.Code);
    }
}
