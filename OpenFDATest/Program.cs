using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OpenFDATest
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string search = "advil";
            SortedDictionary<string, string> temp = new SortedDictionary<string, string>(GetMedInfo(search));
            Console.WriteLine($"Length of incoming data: {temp.Count}");
            foreach (var entry in temp)
            {
                var itemKey = entry.Key;
                var itemValue = entry.Value;
                if (itemKey.IndexOf("table") > -1 || itemKey.IndexOf("open") > -1)
                {
                    continue;
                }
                if (itemKey.IndexOf("ingredient") > -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Matched entry: {entry.Key}");
                    continue;
                }
                if (itemKey.IndexOf("effective_time") > -1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string date = entry.Value;
                    // moved the functionality of getting a date to a new class
                    DateTime lastUpdated = SortDates.parseDate(date);
                    Console.WriteLine(lastUpdated.ToLongDateString());
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"We have the following key: {entry.Key}");
            }
            // TODO: take each medicine and make it into a Medicine item using the class
            Console.ReadKey();
        }
    }
}
