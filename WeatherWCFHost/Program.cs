using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WeatherWCF;

namespace WeatherWCFHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:8000/WeatherWCF/Service");
            ServiceHost selfHost = new ServiceHost(typeof(DegreesService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IDegrees), new WebHttpBinding(), "")
                    .Behaviors.Add(new WebHttpBehavior());

                selfHost.Open();
                Console.WriteLine("The service is ready.");

                Console.WriteLine("Press <Enter> to terminate the service.");
                Console.WriteLine();
                Console.ReadLine();
                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
