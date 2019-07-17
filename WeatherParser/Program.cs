using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;

namespace WeatherParser
{
    class Program
    {
        static void Main(string[] args)
        {
            List<WeatherCity> cities = HtmlParser.Parser();
            foreach (WeatherCity city in cities)
            {
                GenericDataAccess.Execite("test.AddCityWeather",
                    new string[] { "in_city", "in_degreeDay", "in_degreeNight" },
                    new object[] { city.City, city.DegreesDay, city.DegreesNight });
            }
            Console.ReadLine();
        }
    }
}
