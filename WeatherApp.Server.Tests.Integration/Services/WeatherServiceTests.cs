using WeatherApp.Server.Models;
using WeatherApp.Server.Utils.WeatherClient;

namespace WeatherApp.Server.Services;

[TestClass]
public class WeatherServiceTests
{
    [TestMethod]
    public async Task GetCurrentWeatherAsync_GetsCurrentWeather()
    {
        // Arrange
        Location location = new(0, 0, "Location");
        
        CurrentWeather currentWeather = new(
            location,
            new(0),
            0,
            0,
            new(0),
            true,
            DateTime.Now);
        
        IWeatherClient weatherClient = Substitute.For<IWeatherClient>();
        weatherClient
            .GetCurrentWeatherAsync(Arg.Any<Location>())
            .Returns(currentWeather);

        WeatherService weatherService = new(weatherClient);

        // Act
        CurrentWeather res = await weatherService.GetCurrentWeatherAsync(location);

        // Assert
        Assert.AreEqual(currentWeather.Location.Name, res.Location.Name);
    }

    [TestMethod]
    public async Task GetDailyForecastAsync_GetsDailyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Location");

        DailyForecast dailyForecast = new(
            location,
            new(0),
            0,
            0,
            0,
            0,
            DateTime.Now,
            DateTime.Now,
            0,
            0,
            0,
            new(0));

        DailyForecast[] dailyForecasts = new[]
        {
            dailyForecast,
            dailyForecast,
            dailyForecast
        };        

        IWeatherClient weatherClient = Substitute.For<IWeatherClient>();
        weatherClient
            .GetDailyForecastAsync(Arg.Any<Location>())
            .Returns(dailyForecasts);

        WeatherService weatherService = new(weatherClient);

        // Act
        DailyForecast[] res = await weatherService.GetDailyForecastAsync(location);

        // Assert
        Assert.AreEqual(dailyForecasts.Length, res.Length);
        Assert.AreEqual(dailyForecast.Location.Name, res[0].Location.Name);
    }

    [TestMethod]
    public async Task GetHourlyForecastAsync_GetsHourlyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Location");

        HourlyForecast hourlyForecast = new(
            location,
            new(0),
            DateTime.Now,
            0,
            0,
            0,
            0,
            0,
            new(0),
            0,
            true);

        HourlyForecast[] hourlyForecasts = new[]
        {
            hourlyForecast,
            hourlyForecast,
            hourlyForecast
        };

        IWeatherClient weatherClient = Substitute.For<IWeatherClient>();
        weatherClient
            .GetHourlyForecastAsync(Arg.Any<Location>())
            .Returns(hourlyForecasts);

        WeatherService weatherService = new(weatherClient);

        // Act
        HourlyForecast[] res = await weatherService.GetHourlyForecastAsync(location);

        // Assert
        Assert.AreEqual(hourlyForecasts.Length, res.Length);
        Assert.AreEqual(hourlyForecast.Location.Name, res[0].Location.Name);
    }

    [TestMethod]
    public async Task GetCurrentDayForecastAsync_GetsCurrentDayForecast()
    {
        // Arrange
        Location location = new(0, 0, "Location");

        DailyForecast dailyForecast = new(
            location,
            new(0),
            0,
            0,
            0,
            0,
            DateTime.Now,
            DateTime.Now,
            0,
            0,
            0,
            new(0));

        IWeatherClient weatherClient = Substitute.For<IWeatherClient>();
        weatherClient
            .GetCurrentDayForecastAsync(Arg.Any<Location>())
            .Returns(dailyForecast);

        WeatherService weatherService = new(weatherClient);

        // Act
        DailyForecast res = await weatherService.GetCurrentDayForecastAsync(location);

        // Assert
        Assert.AreEqual(dailyForecast.Location.Name, res.Location.Name);
    }

    [TestMethod]
    public async Task GetCurrentDayHourlyForecastAsync_GetsCurrentDayHourlyForecast()
    {
        // Arrange
        Location location = new(0, 0, "Location");

        HourlyForecast hourlyForecast = new(
            location,
            new(0),
            DateTime.Now,
            0,
            0,
            0,
            0,
            0,
            new(0),
            0,
            true);

        HourlyForecast[] hourlyForecasts = new HourlyForecast[24];
        for (int i = 0; i < 24; i++)
            hourlyForecasts[i] = hourlyForecast;

        IWeatherClient weatherClient = Substitute.For<IWeatherClient>();
        weatherClient
            .GetCurrentDayHourlyForecastAsync(Arg.Any<Location>())
            .Returns(hourlyForecasts);

        WeatherService weatherService = new(weatherClient);

        // Act
        HourlyForecast[] res = await weatherService.GetCurrentDayHourlyForecastAsync(location);

        // Assert
        Assert.AreEqual(hourlyForecasts.Length, res.Length);
        Assert.AreEqual(hourlyForecast.Location.Name, res[0].Location.Name);
    }
}
