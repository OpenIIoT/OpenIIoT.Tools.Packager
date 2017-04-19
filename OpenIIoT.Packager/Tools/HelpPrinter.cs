using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.BigFont;

namespace OpenIIoT.Packager.Tools
{
    public static class HelpPrinter
    {
        #region Private Fields

        private static Action Break = () => Console.WriteLine("   │");
        private static Action<string> Prefixed = (s) => Console.WriteLine("   │ " + s);
        private static Action<string> Spaced = (s) => Console.WriteLine("   " + s);

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

            Prefixed("> manifest\t\tGenerate a package manifest file.");
            Prefixed("> package\t\tCreate a package file.");
            Prefixed("> sign\t\tDigitally sign a package file.");
            Prefixed("> verify\t\tVerify the integrity of a package file.");
            Prefixed("> trust\t\tAdd a trust to a signed package file.");
            Prefixed("> verify-trust\tVerify a trusted package file.");

            Break();
            Prefixed("! use 'help <command>' to get more details about that command.");

            PrintFooter();
        }

        private static void PrintFooter()
        {
            Spaced("└───────────────────── ── ─────────────── ─── ─ ─  ─   ─");
        }

        private static void PrintHeader()
        {
            Spaced("┌─────────────────────── ─ ─── ───────────────────── ── ───── ─ ───   ──");
        }

        private static void PrintManifestHelp()
        {
            PrintTitle("Manifest");
            PrintHeader();

            Prefixed("> manifest");
            Break();

            Prefixed("[-h|--hash]\t\t\tMarks files to be hashed during packaging.");
            Prefixed("[-i|--include-resources]\t\tIncludes non-binary/web files.");
            Prefixed("[-d|--directory <directory>]\tDirectory containing payload files.");
            Prefixed("[-o|--output <file>]\t\tOutput manifest file.");

            Break();
            Prefixed("! ex: 'manifest -hi -d \"desktop\\coolPlugin\" -o \"manifest.json\"'");
            PrintFooter();
        }

        private static void PrintPackageHelp()
        {
            PrintTitle("Package");
            PrintHeader();

            Prefixed("> package");
            Break();

            Prefixed("<directory>\tDirectory containing payload files.");
            Prefixed("<manifest>\t\tThe manifest for the package (manifest.json, generate with 'manifest')");
            Prefixed("<output file>\tOutput package file (*.opkg).");

            Break();
            Prefixed("! ex: 'package \"desktop\\coolPlugin\" \"manifest.json\" \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        private static void PrintSignHelp()
        {
            PrintTitle("Sign");
            PrintHeader();

            Prefixed("> sign");
            Break();

            Prefixed("<package>\t\tThe package to sign.");
            Prefixed("<public key file>\tThe ASCII-armored PGP public key file.");
            Prefixed("<private key file>\tThe ASCII-armored PGP private key file.");

            Break();
            Prefixed("! ex: 'sign \"coolPlugin.opkg\" \"publicKey.asc\" \"privateKey.asc\"");
            PrintFooter();
        }

        private static void PrintTitle(string title)
        {
            string[] lines = BigFontGenerator.Generate(title, Font.Graffiti, FontSize.Small);

            Console.WriteLine("\n");

            foreach (string line in lines)
            {
                Console.WriteLine(" " + line);
            }
        }

        private static void PrintTrustHelp()
        {
            PrintTitle("Trust");
            PrintHeader();

            Prefixed("> trust");
            Break();

            Prefixed("<package>\t\tThe package to trust.");
            Prefixed("<private key file>\tThe ASCII-armored PGP private key file.");

            Break();
            Prefixed("! ex: 'verify \"coolPlugin.opkg\" \"privateKey.asc\"'");
            PrintFooter();
        }

        private static void PrintVerifyHelp()
        {
            PrintTitle("Verify");
            PrintHeader();

            Prefixed("> verify");
            Break();

            Prefixed("<package>\t\tThe package to verify.");

            Break();
            Prefixed("! ex: 'verify \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        private static void PrintVerifyTrustHelp()
        {
            PrintTitle("Verify-Trust");
            PrintHeader();

            Prefixed("> verify-trust");
            Break();

            Prefixed("<package>\t\tThe package to verify.");

            Break();
            Prefixed("! ex: 'verify-trust \"coolPlugin.opkg\"'");
            PrintFooter();
        }

        #endregion Private Methods
    }
}