using System;
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
            Console.ReadKey();
        }
    }
}
