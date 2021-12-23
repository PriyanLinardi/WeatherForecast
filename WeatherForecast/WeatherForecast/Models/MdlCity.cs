using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeatherForecast.Models
{
    public class MdlCity
    {
        #region Properties
        public string CityName { get; set; }
        #endregion

        #region Public Static Method
        public static List<SelectListItem> GetSelectListItem(MdlCountry country)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            foreach (var city in country.CityList)
            {
                SelectListItem item = new SelectListItem() { Value = city.CityName, Text = city.CityName };
                result.Add(item);
            }
            return result;
        }
        #endregion
    }
}