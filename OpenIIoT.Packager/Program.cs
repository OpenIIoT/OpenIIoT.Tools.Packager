using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Dictionary<string, string> parsedArgs = ParseArgs(Environment.CommandLine);

            foreach (string key in parsedArgs.Keys)
            {
                Console.WriteLine("Key: " + key + " value: " + parsedArgs[key]);
            }

            Console.ReadKey();
        }

        private static Dictionary<string, string> ParseArgs(string args)
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>();

            string regEx = "(?:[-]{1,2}|\\/)([\\w-]+)[=|:]?(\\w\\S*|\\\".*\\\"|\\\'.*\\\')?";

            foreach (Match match in Regex.Matches(String.Join(" ", args), regEx))
            {
                if (match.Groups.Count == 3)
                {
                    if (!retVal.ContainsKey(match.Groups[1].Value))
                    {
                        retVal.Add(match.Groups[1].Value, match.Groups[2].Value);
                    }
                }
            }

            return retVal;
        }
    }
}