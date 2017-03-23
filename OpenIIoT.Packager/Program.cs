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
        [Argument("generate-manifest", "When specified, generates a blank manifest file.", false)]
        private static string GenerateManifest { get; set; }

        [Argument("five", "five")]
        private static int Five { get; set; }

        private static void Main(string[] args)
        {
            new Arguments(Environment.CommandLine).Parse();

            Console.WriteLine("GenerateManifest: " + GenerateManifest);
            Console.WriteLine("Five: " + Five);

            Console.ReadLine();
        }
    }
}