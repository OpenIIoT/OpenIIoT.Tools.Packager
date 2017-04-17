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
        #region Private Fields

        private static Action<string> c = (s) => Console.WriteLine(s);

        #endregion Private Fields

        #region Public Methods

        public static void PrintHelp(string command = default(string))
        {
            switch (command)
            {
                case "manifest":
                    PrintManifestHelp();
                    return;

                case "package":
                    PrintPackageHelp();
                    return;

                case "sign":
                    PrintSignHelp();
                    return;

                case "verify":
                    PrintVerifyHelp();
                    return;

                case "trust":
                    PrintTrustHelp();
                    return;

                case "verify-trust":
                    PrintVerifyTrustHelp();
                    return;

                default:
                    PrintCommands();
                    return;
            }
        }

        #endregion Public Methods

        #region Private Methods

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

        private static void PrintFooter()
        {
            c("■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■ ■  ■■■ ");
        }

        private static void PrintHeader()
        {
            c("■■■■■■■■■■■■■■■■■■■■■■■■■■■■ ■ ■■■■ ■  ■  ■  ■■■ ■     ■");
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
            c(" <directory>\tDirectory containing payload files.");
            c(" <manifest>\tThe manifest for the package (manifest.json, generate with 'manifest')");
            c(" <output file>\tOutput package file (*.opkg).");

            c("\n ! ex: 'package \"desktop\\coolPlugin\" \"manifest.json\" \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        private static void PrintSignHelp()
        {
            PrintTitle("Sign");
            PrintHeader();

            c(" > sign\n");
            c(" <package>\t\tThe package to sign.");
            c(" <public key file>\tThe ASCII-armored PGP public key file.");
            c(" <private key file>\tThe ASCII-armored PGP private key file.");

            c("\n ! ex: 'sign \"coolPlugin.opkg\" \"publicKey.asc\" \"privateKey.asc\"");
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

        private static void PrintTrustHelp()
        {
            PrintTitle("Trust");
            PrintHeader();

            c(" > trust\n");
            c(" <package>\t\tThe package to trust.");
            c(" <private key file>\tThe ASCII-armored PGP private key file.");

            c("\n ! ex: 'verify \"coolPlugin.opkg\" \"privateKey.asc\"'");
            PrintFooter();
        }

        private static void PrintVerifyHelp()
        {
            PrintTitle("Verify");
            PrintHeader();

            c(" > verify\n");
            c(" <package>\tThe package to verify.");

            c("\n ! ex: 'verify \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        private static void PrintVerifyTrustHelp()
        {
            PrintTitle("Verify-Trust");
            PrintHeader();

            c(" > verify-trust\n");
            c(" <package>\tThe package to verify.");

            c("\n ! ex: 'verify-trust \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        #endregion Private Methods
    }
}