using System;
using System.Collections.Generic;
using System.Linq;
using WeatherParser;

namespace WeatherWCF
{
    public class DegreesService : IDegrees
    {
        public static List<City> cityList = null;

        public List<City> GetCities()
        {
            return GenericDataAccess.ExeciteSelect("test.GetCities",
                    new string[] { },
                    new object[] { })?
                    .Select(r => new City { Id = (int)r["id"], Name = r["name"].ToString() }).ToList() ?? null;
        }

        public Degrees GetDegrees(string cityId)
        {
            int result = 0;
            if (int.TryParse(cityId, out result))
            {
                return GenericDataAccess.ExeciteSelect("test.GetCityDegrees",
                        new string[] { "id" },
                        new object[] { result })?
                        .Select(r => new Degrees { Day = (int)r["degrees_day"], Night = (int)r["degrees_night"] })
                        .FirstOrDefault() ?? null;
            }
            else
                return null;
        }

    }
}
