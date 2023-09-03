using Microsoft.Extensions.Primitives;
using WeatherApp.Server.Models;
using WeatherApp.Server.Services;

namespace WeatherApp.Server.Controllers;

[TestClass]
public class WeatherControllerTests
{
    [TestMethod]
    public async Task GetForecast_GetsDaily()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var dailyForecasts = new DailyForecast[7]
            .Select(_ => new DailyForecast(
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
                new(0)))
            .ToArray();
        
        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherService
            .GetDailyForecastAsync(Arg.Any<Location>())
            .Returns(dailyForecasts);
        
        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();
        
        // Assert
        Assert.IsInstanceOfType(res, typeof(OkObjectResult));
        
        DailyForecast[] content = (DailyForecast[])((OkObjectResult)res).Value!;
        Assert.AreEqual(
            location.Name,
            content[0].Location.Name);
        Assert.AreEqual(7, content.Length);
    }

    [TestMethod]
    public async Task GetForecast_GetsHourly()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var hourlyForecasts = new HourlyForecast[168]
            .Select(_ => new HourlyForecast(
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
                true))
            .ToArray();

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherService
            .GetHourlyForecastAsync(Arg.Any<Location>())
            .Returns(hourlyForecasts);

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "hourly" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(OkObjectResult));

        HourlyForecast[] content = (HourlyForecast[])((OkObjectResult)res).Value!;
        Assert.AreEqual(
            location.Name,
            content[0].Location.Name);
        Assert.AreEqual(168, content.Length);
    }

    [TestMethod]
    public async Task GetForecast_GetsCurrentDay()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var dailyForecast = new DailyForecast(
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

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherService
            .GetCurrentDayForecastAsync(Arg.Any<Location>())
            .Returns(dailyForecast);

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "current-daily" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(OkObjectResult));

        DailyForecast content = (DailyForecast)((OkObjectResult)res).Value!;
        Assert.AreEqual(
            location.Name,
            content.Location.Name);
    }

    [TestMethod]
    public async Task GetForecast_GetsCurrentDayHourly()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var hourlyForecasts = new HourlyForecast[24]
            .Select(_ => new HourlyForecast(
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
                true))
            .ToArray();

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherService
            .GetCurrentDayHourlyForecastAsync(Arg.Any<Location>())
            .Returns(hourlyForecasts);

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "current-hourly" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(OkObjectResult));

        HourlyForecast[] content = (HourlyForecast[])((OkObjectResult)res).Value!;
        Assert.AreEqual(
            location.Name,
            content[0].Location.Name);
        Assert.AreEqual(24, content.Length);
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidType()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");
        
        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "invalid" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidLatitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", "invalid" },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidLongitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", location.Latitude.ToString() },
                { "long", "invalid" },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public async Task GetForecast_FailsOnMissingType()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingLatitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingLongitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", location.Latitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingName()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public async Task GetForecast_FailsOnMissingTimezone()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "type", "daily" },
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name }
            });

        // Act
        IActionResult res = await weatherController.GetForecast();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCurrent_GetsCurrentWeather()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var currentWeather = new CurrentWeather(
            location,
            new(0),
            0,
            0,
            new(0),
            true,
            DateTime.Now);

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherService
            .GetCurrentWeatherAsync(Arg.Any<Location>())
            .Returns(currentWeather);

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(OkObjectResult));

        CurrentWeather content = (CurrentWeather)((OkObjectResult)res).Value!;
        Assert.AreEqual(
            location.Name,
            content.Location.Name);
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnInvalidLatitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", "invalid" },
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnInvalidLongitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "long", "invalid" },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingLatitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "long", location.Longitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingLongitude()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "name", location.Name },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingName()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "tz", location.Timezone }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }
    
    [TestMethod]
    public async Task GetCurrent_FailsOnMissingTimezone()
    {
        // Arrange
        Location location = new(-27.470125, 153.021072, "Brisbane, Australia", "AEST");

        var logger = Substitute.For<ILogger<WeatherController>>();
        var weatherService = Substitute.For<IWeatherService>();
        WeatherController weatherController = new(logger, weatherService)
        {
            ControllerContext = new()
            {
                HttpContext = new DefaultHttpContext()
            }
        };

        weatherController.Request.Query = new QueryCollection(
            new Dictionary<string, StringValues>()
            {
                { "lat", location.Latitude.ToString() },
                { "long", location.Longitude.ToString() },
                { "name", location.Name }
            });

        // Act
        IActionResult res = await weatherController.GetCurrent();

        // Assert
        Assert.IsInstanceOfType(res, typeof(BadRequestObjectResult));
    }
}
