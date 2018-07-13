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
            Console.WriteLine("OpenFDA Search Tool by Cooper");
            inquiry();
        }

        static void inquiry()
        {
            Console.Write("Type in medicine to look up or \"quit\" to quit\n> ");
            string search = Console.ReadLine();
            if (search.ToLower().Equals("quit"))
            {
                Environment.Exit(0);
            }
            SortedDictionary<string, string> temp = new SortedDictionary<string, string>(GetMedInfo(search));
            SortedDictionary<string, string> result = new SortedDictionary<string, string>();
            foreach (var entry in temp)
            {
                var itemKey = entry.Key;
                var itemValue = entry.Value;
                foreach (string term in FilterEntries.ListEntries())
                {
                    if (itemKey.Equals(term))
                    {
                        result.Add(itemKey, itemValue);
                    }
                }
            }
            foreach (var item in result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                // remove the underscores
                Console.Write($"{item.Key.ToUpper()} ");
                Console.ForegroundColor = ConsoleColor.White;
                // format the item.Value to have no braces
                Console.WriteLine($"{item.Value}");
            }
            inquiry();
        }
    }
}
