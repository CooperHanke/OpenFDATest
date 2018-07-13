using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace OpenFDATest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string url = "https://api.fda.gov/drug/label.json?search=";
            //string search = "caffeine";
            var request = (HttpWebRequest)WebRequest.Create("https://api.fda.gov/drug/label.json?search=caffeine&limit=1");

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            var response = (HttpWebResponse)request.GetResponse();

            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            Console.WriteLine("Finished reading the response");

            try
            {
                JObject results = JObject.Parse(content);
                var test = results["meta"];
                var test2 = results["results"][0]; // grab the first result then
                //Console.WriteLine($"Type of results: {test["effective_date"].Type}");
                int count = 0; // count the keys then
                foreach (JProperty prop in test2)
                {
                    Console.WriteLine(prop.Name);
                    count++;
                }
                Console.WriteLine($"Number of keys in our results: {count}"); // find out the number of keys bc we will need those later on

                count = 0;
                foreach (JProperty prop in test2)
                {
                    if (prop.Name.Equals("openfda")) // and now we have proven we can sift by keys
                    {
                        Console.WriteLine("Nope, found openfda, count should be one less now");
                        continue;
                    }
                    count++;
                }
                Console.WriteLine($"Number of keys in our results: {count} (should be less really)"); // find out the number of keys bc we will need those later on
                Console.WriteLine($"Type of results[\"meta\"] is {test.Type}"); // meta = object
                Console.WriteLine($"Type of results[\"results\"] is {test2.Type}"); // results = array
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Console.WriteLine("Failed to get the JSON! Nooooo!");
                Console.WriteLine($"{ex.Message}");
            }
            Console.WriteLine("Press enter to end the program...");
            Console.ReadKey();
        }
    }
}
