using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    internal class Options
    {
        internal ProgramAction Action { get; }
        internal DirectoryInfo PayloadDirectory { get; }
        internal FileInfo ManifestFile { get; }
        internal FileInfo PrivateKeyFile { get; }

        internal static Options Parse(string[] args)
        {
            return new Options();
        }
    }
}