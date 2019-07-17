using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherClient.Models
{
    public class Weather
    {
        public int CityId { get; set; }
        public int Day { get; set; }
        public int Night { get; set; }
    }
}
