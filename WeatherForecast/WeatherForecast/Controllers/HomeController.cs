using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<MdlCountry> countryList = MdlCountry.GetCountryWithCityList();
            List<SelectListItem> countryItems = MdlCountry.GetSelectListItem(countryList);
            List<SelectListItem> cityItems =  new List<SelectListItem>();
            if (countryList.Count > 0 && countryList[0].CityList.Count > 0)
            {
                cityItems = MdlCity.GetSelectListItem(countryList[0]);
            }

            WeatherController.countryWithCityGlobalList = countryList;
            ViewBag.CountryList = countryItems;
            ViewBag.CityList = cityItems;

            return View();
        }
    }
}
