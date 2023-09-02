namespace WeatherApp.Server.Models;

[TestClass]
public class CardinalTests
{
    [TestMethod]
    [DataRow(-60, "West-Northwest")]
    [DataRow(0, "North")]
    [DataRow(60, "East-Northeast")]
    [DataRow(120, "East-Southeast")]
    [DataRow(180, "South")]
    [DataRow(240, "West-Southwest")]
    [DataRow(300, "West-Northwest")]
    [DataRow(360, "North")]
    [DataRow(420, "East-Northeast")]
    public void Direction_DisplaysCorrectDirection(double angle, string expected)
    {
        // Arrange
        Cardinal cardinal = new(angle);

        // Act
        string direction = cardinal.Direction;

        // Assert
        Assert.AreEqual(expected, direction);
    }

    [TestMethod]
    [DataRow(-60, "WNW")]
    [DataRow(0, "N")]
    [DataRow(60, "ENE")]
    [DataRow(120, "ESE")]
    [DataRow(180, "S")]
    [DataRow(240, "WSW")]
    [DataRow(300, "WNW")]
    [DataRow(360, "N")]
    [DataRow(420, "ENE")]
    public void DirectionInitials_DisplaysCorrectInitial(double angle, string expected)
    {
        // Arrange
        Cardinal cardinal = new(angle);

        // Act
        string initial = cardinal.DirectionInitial;

        // Assert
        Assert.AreEqual(expected, initial);
    }
}
