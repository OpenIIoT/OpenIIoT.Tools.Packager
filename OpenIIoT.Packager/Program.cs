using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommandLine;

namespace OpenIIoT.Packager
{
    internal class Program
    {
        [Argument("generate-manifest", "When specified, generates a blank manifest file.")]
        private static string GenerateManifest { get; set; }

        [Argument("five", "five")]
        private static int Five { get; set; }

        private static void Main(string[] args)
        {
            Arguments.Populate();

            var argdict = Arguments.Parse();

            foreach (string key in argdict.Keys)
            {
                Console.WriteLine("Key: " + key + "\tValue: " + argdict[key]);
            }

            Console.WriteLine("GenerateManifest: " + GenerateManifest);
            Console.WriteLine("Five: " + Five);

            Console.ReadLine();
        }
    }
}