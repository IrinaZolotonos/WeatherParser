using System;
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
        public static List<WeatherCity> Parser()
        {
            var cities = new List<WeatherCity>();

            string url = ConfigurationSettings.UrlParser;
            string html = string.Empty;

            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            html = streamReader.ReadToEnd();

            Regex sectionRegex = new Regex(@"", RegexOptions.Compiled | RegexOptions.Singleline);
            foreach (var section in sectionRegex.Matches(html).Cast<Match>().Select(matches => new
            {
                section = WebUtility.HtmlDecode(matches.Groups["section"].ToString())
            }
            ))

            return new List<WeatherCity>() { new WeatherCity { City="asad", DegreesDay=10, DegreesNight=-10} };
        }
    }
}