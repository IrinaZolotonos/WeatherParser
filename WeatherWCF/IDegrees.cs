using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WeatherParser;

namespace WeatherWCF
{
    [ServiceContract]
    public interface IDegrees
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetCities")]
        List<City> GetCities();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
                    UriTemplate = "GetDegrees/{cityId}")]
        Degrees GetDegrees(string cityId);

    }

    [DataContract]
    public class City
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    public class Degrees
    {
        [DataMember]
        public int Day { get; set; }
        [DataMember]
        public int Night { get; set; }
    }

}
