using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal class Options
    {
    }

    internal class Program
    {
        [DisplayName("generate-manifest")]
        private static string GenerateManifest { get; set; }

        [DisplayName("five")]
        private static int Five { get; set; }

        private static void Main(string[] args)
        {
            new CommandLineArguments(Environment.CommandLine).ParseInto(typeof(Program));

            Console.WriteLine("GenerateManifest: " + GenerateManifest);
            Console.WriteLine("Five: " + Five);

            Console.ReadLine();
        }
    }
}