using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal enum CommandLineAction
    {
        GenerateManifest,
        CreatePackage
    }

    internal class CommandLineOptions
    {
        [Option('a', "action")]
        internal CommandLineAction Action { get; }

        [Option('p', "payload")]
        internal DirectoryInfo PayloadDirectory { get; }

        [Option('m', "manifest")]
        internal FileInfo ManifestFile { get; }

        [Option('k', "key")]
        internal FileInfo PrivateKeyFile { get; }

        internal static CommandLineOptions Parse(string[] args)
        {
            return new CommandLineOptions();
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        private class OptionAttribute : Attribute
        {
            private char ShortName { get; set; }
            private string LongName { get; set; }

            internal OptionAttribute(char shortName, string longName)
            {
                ShortName = shortName;
                LongName = longName;
            }
        }
    }
}