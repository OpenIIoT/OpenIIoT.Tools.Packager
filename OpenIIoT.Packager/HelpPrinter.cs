using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BigFont;

namespace OpenIIoT.Packager
{
    public static class HelpPrinter
    {
        private static Action<string> c = (s) => Console.WriteLine(s);

        public static void PrintHelp(string command = default(string))
        {
            if (command == "manifest")
            {
                PrintManifestHelp();
            }
            else if (command == "package")
            {
                PrintPackageHelp();
            }
            else
            {
                PrintCommands();
            }
        }

        private static void PrintManifestHelp()
        {
            PrintTitle("Manifest");
            PrintHeader();

            c(" > manifest\n");
            c(" [-h|--hash]\t\t\tMarks files to be hashed during packaging.");
            c(" [-i|--include-resources]\tIncludes non-binary/web files.");
            c(" [-d|--directory <directory>]\tDirectory containing payload files.");
            c(" [-o|--output <file>]\t\tOutput manifest file.");

            c("\n ! ex: 'manifest -hi -d \"desktop\\coolPlugin\" -o \"manifest.json\"'");
            PrintFooter();
        }

        private static void PrintPackageHelp()
        {
            PrintTitle("Package");
            PrintHeader();

            c(" > package\n");
            c(" <-m|--manifest>\t\tThe manifest for the package (manifest.json, generate with 'manifest')");
            c(" <-d|--directory <directory>>\tDirectory containing payload files.");
            c(" <-o|--output <file>>\t\tOutput package file (*.opkg).");

            c("\n ! ex: 'package -m \"manifest.json\" -d \"desktop\\coolPlugin\" -o \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        private static void PrintCommands()
        {
            PrintTitle("Commands");
            PrintHeader();

            c(" > manifest\tGenerate a package manifest file.");
            c(" > package\tCreate a package file.");
            c(" > sign\t\tDigitally sign a package file.");
            c(" > verify\tVerify the integrity of a package file.");
            c(" > trust\tAdd a trust to a signed package file.");
            c(" > verify-trust\tVerify a trusted package file.");

            c("\n ! use 'help <command>' to get more details about that command.");

            PrintFooter();
        }

        private static void PrintTitle(string title)
        {
            string[] lines = BigFontGenerator.Generate(title, Font.Graffiti, FontSize.Small);

            c("\n");

            foreach (string line in lines)
            {
                c(" " + line);
            }
        }

        private static void PrintHeader()
        {
            c("■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■ ■  ■  ■  ■■■ ■     ■");
        }

        private static void PrintFooter()
        {
            c("■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■ ■  ■■■ ");
        }
    }
}