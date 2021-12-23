using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeatherForecast.Controllers
{
    public class APIController : ApiController
    {
        public static string GetAPIJsonResult(string url, string method)
        {
            string resultUrl = string.Empty;
            try
            {
                WebRequest requestObject = WebRequest.Create(url);
                requestObject.Method = method;
                HttpWebResponse responseObject = null;
                responseObject = (HttpWebResponse)requestObject.GetResponse();

                using (Stream stream = responseObject.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    resultUrl = sr.ReadToEnd();
                    sr.Close();
                }
                return resultUrl;
            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }
        }
    }
}