namespace WeatherApp.Server.Controllers;

[TestClass]
public class WeatherControllerTests
{
    [TestMethod]
    public async Task GetForecast_GetsDaily()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_GetsHourly()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_GetsCurrentDay()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_GetsCurrentDayHourly()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidType(string type)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidLatitude(string latitude)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnInvalidLongitude(string longitude)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
    
    [TestMethod]
    public async Task GetForecast_FailsOnMissingType()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingLatitude()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingLongitude()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnMissingName()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetForecast_FailsOnNoBody()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_GetsCurrentWeather()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnInvalidLatitude(string latitude)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnInvalidLongitude(string longitude)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingLatitude()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingLongitude()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnMissingName()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    [TestMethod]
    public async Task GetCurrent_FailsOnNoBody()
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }
}
