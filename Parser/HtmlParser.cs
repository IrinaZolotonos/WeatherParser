using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace WeatherParser
{
    public class HtmlParser
    {
        public static int? ConvertDegree(string degree)
        {
            int result = 0;
            if (int.TryParse(degree.Replace(" ", ""), out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public static List<WeatherCity> Parser()
        {
            var cities = new List<WeatherCity>();

            string url = ConfigurationSettings.UrlParser;
            string html = string.Empty;

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            html = streamReader.ReadToEnd();

            Regex sectionRegex = new Regex(@"City frame[\s\S]*?End City frame", 
                RegexOptions.Compiled | RegexOptions.Singleline);
            var section = sectionRegex.Matches(html)
                .Cast<Match>()
                .Select(matches => new { section = WebUtility.HtmlDecode(matches.Groups[0].ToString()) })
                .FirstOrDefault();

            Regex hyperlinkRegex = new Regex(@"<a\s*?href\s*?=.*?\""/([^/]*).*?data-name\s*?=\""([^\""]*)",
                RegexOptions.Compiled | RegexOptions.Singleline);
            var citiesLink = hyperlinkRegex.Matches(section.section)
                .Cast<Match>()
                .Select(matches => new {
                    link = WebUtility.HtmlDecode(matches.Groups[1].ToString()),
                    name = WebUtility.HtmlDecode(matches.Groups[2].ToString())
                })
                .ToList();

            foreach (var city in citiesLink)
            {
                string urlCity = url + "/" + city.link;
                HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(urlCity);
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                StreamReader strReader = new StreamReader(httpResponse.GetResponseStream());
                html = strReader.ReadToEnd();

                Regex sectionCityRegex = new Regex(@"Завтра.*?<svg",
                RegexOptions.Compiled | RegexOptions.Singleline);
                var sectionCity = sectionCityRegex.Matches(html)
                    .Cast<Match>()
                    .Select(matches => new { section = WebUtility.HtmlDecode(matches.Groups[0].ToString()) })
                    .ToList();

                Regex degreesRegex = new Regex(@"unit_temperature_c[^>]*>([^<]*)",
                    RegexOptions.Compiled | RegexOptions.Singleline);

                const int DEGREES_COUNT = 2;
                foreach (var varSectionCity in sectionCity)
                {
                    var degrees = degreesRegex.Matches(varSectionCity.section)
                    .Cast<Match>()
                    .Select(matches => new { value = WebUtility.HtmlDecode(matches.Groups[1].ToString()) })
                    .ToList();

                    if (degrees.Count == DEGREES_COUNT)
                    {
                        int? degreeNight = ConvertDegree(degrees[0].value);
                        int? degreeDay = ConvertDegree(degrees[1].value);

                        if (degreeNight.HasValue && degreeDay.HasValue)
                        {
                            cities.Add(new WeatherCity
                            {
                                City = city.name,
                                DegreesNight = degreeNight.Value,
                                DegreesDay = degreeDay.Value
                            });
                        }
                    }
                }
            }

            return cities;
        }
    }
}