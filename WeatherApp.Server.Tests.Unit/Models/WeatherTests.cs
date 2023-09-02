namespace WeatherApp.Server.Models;

[TestClass]
public class WeatherTests
{
    [TestMethod]
    [DataRow(0, "Clear sky")]
    [DataRow(45, "Fog")]
    [DataRow(75, "Heavy snow fall")]
    [DataRow(99, "Thunderstorm with heavy hail")]
    public void Description_DisplaysCorrectDescription(int code, string expected)
    {
        // Arrange
        Weather weather = new(code);

        // Act
        string description = weather.Description;

        // Assert
        Assert.AreEqual(expected, description);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(10)]
    [DataRow(100)]
    public void Descriptions_InvalidCodeThrowsException(int code)
    {
        // Arrange
        Weather weather = new(code);

        // Act
        string description() => weather.Description;

        // Assert
        Assert.ThrowsException<ArgumentException>(description);
    }
}
