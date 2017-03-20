using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Options options = Options.Parse(args);

            Console.WriteLine("Action: " + options.Action);
            Console.WriteLine("PayloadDirectory: " + options.PayloadDirectory);
            Console.WriteLine("ManifestFile: " + options.ManifestFile);
            Console.WriteLine("PrivateKeyFile: " + options.PrivateKeyFile);
        }
    }
}