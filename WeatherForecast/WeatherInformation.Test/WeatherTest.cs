using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherForecast.Controllers;

namespace WeatherInformation.Test
{
    [TestClass]
    public class WeatherTest
    {
        public string countryCode = "INA";
        public string cityName = "Jakarta";

        [TestMethod]
        public void IsCountryAndCityValid_CountryCodeAndCityCorrect_ReturnsTrue()
        {
            // Arrange
            var weather = new WeatherController();

            // Act
            var result = weather.IsCountryAndCityValid(countryCode, cityName);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetWeatherInformation_CallAPIAndConvertJsonCorrect_ReturnsTrue()
        {
            // Arrange
            var weather = new WeatherController();

            // Act
            bool result = true;
            var weatherInfo = weather.GetWeatherInformation(countryCode, cityName);
            if (weatherInfo.Contains("Error"))
                result = false;

            // Assert
            Assert.IsTrue(result);
        }
    }
}
