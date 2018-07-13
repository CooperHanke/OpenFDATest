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
    /*
     * This class is meant to separate the logic of getting the medicine data from the rest of the program
     */

    partial class Program
    {
        private static SortedDictionary<string, string> GetMedInfo(string desiredSearch)
        {
            string url = "https://api.fda.gov/drug/label.json?search=";
            var request = (HttpWebRequest)WebRequest.Create($"{url}{desiredSearch}&limit=1");

            request.Method = "GET";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sorry, that medicine was not found, please run the application again!");
                Environment.Exit(Convert.ToInt16(ex.Status));
            }

            string content = string.Empty;
            using (var stream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(stream))
                {
                    content = sr.ReadToEnd();
                }
            }
            SortedDictionary<string, string> listings = SortedMedInfo(content);
            return listings;
        }
        
        private static SortedDictionary<string, string> SortedMedInfo (string content)
        {
            SortedDictionary<string, string> temp = new SortedDictionary<string, string>();
            try
            {
                JObject results = JObject.Parse(content);
                var meta = results["meta"];
                var data = results["results"][0]; // grab the first result then
                foreach (JProperty prop in data)
                {
                    temp.Add(prop.Name, prop.Value.ToString());
                }
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Console.WriteLine("Failed to get the JSON! Nooooo!");
                Console.WriteLine($"{ex.Message}");
            }
            return temp;
        }
    }
}
