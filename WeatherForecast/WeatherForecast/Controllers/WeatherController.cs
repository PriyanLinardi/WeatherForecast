using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        public static List<MdlCountry> countryWithCityGlobalList = new List<MdlCountry>();
        public static string APP_KEY = "859f1be706574b7e5b7dcfe0dd32a967";

        [HttpPost]
        public string GetCityList(string countryCode)
        {
            List<string> cityList = new List<string>();
            try
            {
                if (countryWithCityGlobalList.Count == 0)
                {
                    countryWithCityGlobalList = MdlCountry.GetCountryWithCityList();
                }

                cityList = countryWithCityGlobalList.Where(a => a.CountryCode == countryCode).Select(b => b.CityList.Select(c => c.CityName).ToList()).FirstOrDefault();
                return string.Join(",", cityList);
            }
            catch (Exception)
            {
                return "err";
            }
        }

        [HttpPost]
        public string GetWeatherInformation(string countryCode, string cityName)
        {
            try
            {
                bool isValid = IsCountryAndCityValid(countryCode, cityName);
                if (!isValid)
                    return "Error : Invalid Country or City";

                string apiUrl = String.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", cityName, APP_KEY);
                string resultUrl = APIController.GetAPIJsonResult(apiUrl, "POST");
                JObject json = JObject.Parse(resultUrl);

                var windSpeed = (float)json["wind"]["speed"];
                var windDeg = (float)json["wind"]["speed"];
                var visibility = (float)json["visibility"];

                var skyConditionWeather = json["weather"];
                string weatherMain = string.Empty;
                string weatherDescription = string.Empty;
                foreach (var item in skyConditionWeather)
                {
                    weatherMain = (string)item["main"];
                    weatherDescription = (string)item["description"];
                }

                var pressure = (int)json["main"]["pressure"];
                var humidity = (int)json["main"]["humidity"];
                var temperature = (float)json["main"]["temp"];
                var locationCoordLon = (string)json["coord"]["lon"];
                var locationCoordLat = (string)json["coord"]["lat"];

                string result = $@"Longitude : {locationCoordLon}  Latitude : {locationCoordLat} <br/> ";
                result += $@"Speed : {windSpeed}   Deg : {windDeg} <br/> ";
                result += $@"Visibility : {visibility} <br/>";
                result += $@"Sky Condition : {weatherMain} ; {weatherDescription} <br/>";
                result += $@"Temperature : {temperature} <br/>";
                result += $@"Relative Humidity : {humidity}% <br/>";
                result += $@"Pressure : {pressure}";

                return result;
            }
            catch (Exception ex)
            {
                return "Error :" +  ex.Message;
            }
        }

        public bool IsCountryAndCityValid(string countryCode, string cityName)
        {
            List<MdlCountry> countryList = MdlCountry.GetCountryWithCityList();
            if (countryCode == null && cityName == null)
                return false;
            else
            {
                MdlCountry country = countryList.Where(a => a.CountryCode == countryCode).FirstOrDefault();
                if (country == null)
                    return false;
                else if (!country.CityList.Exists(a => a.CityName == cityName))
                    return false;
                else return true;
            } 
        }
    }
}