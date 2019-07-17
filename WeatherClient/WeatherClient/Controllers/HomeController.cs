using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherClient.Models;
using Microsoft.Extensions.Configuration;

namespace WeatherClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly string url;
        private readonly WebClient webClient;

        public HomeController(IConfiguration configuration)
        {
            url = configuration["WCF"];
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<City> cities = new List<City>();
            try
            {
                string reply = webClient.DownloadString(String.Format("{0}{1}", url, "GetCities"));
                if (reply != "")
                {
                    cities = JsonConvert.DeserializeObject<List<City>>(reply);
                }
            }
            catch 
            {
                
            }
            return View(cities);
        }

        [HttpPost]
        public JsonResult GetWeather()
        {
            StreamReader sr = new StreamReader(Request.Body);
            string data = sr.ReadToEnd();

            Weather weather = new Weather() { CityId = int.Parse(data) };

            try
            {
                string reply = webClient.DownloadString(String.Format("{0}{1}/{2}", url, "GetDegrees", data));
                if (reply != "")
                {
                    weather = JsonConvert.DeserializeObject<Weather>(reply);
                }
            }
            catch
            {

            }
            return Json(weather);
        }
    }
}