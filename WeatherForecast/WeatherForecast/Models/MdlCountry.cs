using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Controllers;


namespace WeatherForecast.Models
{
    public class MdlCountry
    {
        #region Properties
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public List<MdlCity> CityList { get; set; }
        #endregion

        #region Public Static Method
        public static List<MdlCountry> GetCountryWithCityList()
        {
            List<MdlCountry> result = new List<MdlCountry>();
            string countryJsonUrl = String.Format("https://raw.githubusercontent.com/russ666/all-countries-and-cities-json/master/countries.json");
            string resultUrl = APIController.GetAPIJsonResult(countryJsonUrl, "GET");

            dynamic dynJson = JsonConvert.DeserializeObject(resultUrl);
            foreach (var item in dynJson)
            {
                MdlCountry country = new MdlCountry();
                country.CountryName = item.Name;
                if (!result.Exists(a => a.CountryCode == country.CountryName.Substring(0, 3).ToUpper()))
                    country.CountryCode = country.CountryName.Substring(0, 3).ToUpper();
                else
                    country.CountryCode = country.CountryName.Substring(0, 2).ToUpper() + country.CountryName.Substring(country.CountryName.Length - 1, 1).ToUpper();

                #region Get City by each country
                country.CityList = new List<MdlCity>();
                foreach (var city in item.Value)
                {
                    MdlCity cityAdd = new MdlCity();
                    cityAdd.CityName = (string)city;
                    country.CityList.Add(cityAdd);
                }
                #endregion
                result.Add(country);
            }

            return result;
        }

        public static List<SelectListItem> GetSelectListItem(List<MdlCountry> countryList)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var country in countryList)
            {
                SelectListItem item = new SelectListItem() { Value = country.CountryCode, Text = country.CountryName };
                result.Add(item);
            }
            return result;
        }
        #endregion
    }
}