using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherParser
{
    public class WeatherCity
    {
        public int CityId { get; set; }
        public string City { get; set; }
        public int DegreesDay { get; set; }
        public int DegreesNight { get; set; }
    }
}
