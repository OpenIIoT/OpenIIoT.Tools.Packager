using OpenIIoT.SDK.Package.Manifest;
using System;
using Utility.CommandLine;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace OpenIIoT.Packager
{
    internal class Program
    {
        #region Private Properties

        [Argument('d', "directory")]
        private static string InputDirectory { get; set; }

        [Argument('i', "include-resources")]
        private static bool IncludeResources { get; set; }

        [Argument('h', "hash")]
        private static bool Hash { get; set; }

        [Argument('g', "generate")]
        private static bool Generate { get; set; }

        [Argument('o', "output")]
        private static string OutputFile { get; set; }

        [Operands]
        private static string[] Operands { get; set; }

        [Argument('p', "package")]
        private static bool Package { get; set; }

        #endregion Private Properties

        #region Private Methods

        public static PackageManifest GenerateManifest(string directory = "")
        {
            PackageManifest manifest = PackageManifestFactory.GetDefault();
            manifest.Files = new List<IPackageManifestFile>();

            if (directory != string.Empty && Directory.Exists(directory))
            {
                foreach (string file in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
                {
                    if (File.Exists(file))
                    {
                        byte[] data = File.ReadAllBytes(file);
                        byte[] result;
                        SHA512 shaM = new SHA512Managed();
                        result = shaM.ComputeHash(data);

                        var hashedInputStringBuilder = new System.Text.StringBuilder(128);
                        foreach (var b in result)
                            hashedInputStringBuilder.Append(b.ToString("X2"));
                        string res = hashedInputStringBuilder.ToString();

                        Console.WriteLine("File: " + file);
                        Console.WriteLine("Hash: " + res);

                        PackageManifestFile manifestFile = new PackageManifestFile();

                        manifestFile.Source = Utility.GetRelativePath(directory, file);

                        if (Utility.GetFileType(file) != default(PackageManifestFileType))
                        {
                            manifestFile.Type = Utility.GetFileType(file);
                        }

                        manifestFile.Hash = res;

                        manifest.Files.Add(manifestFile);
                    }
                }
            }
            else
            {
                Console.WriteLine("Couldn't find directory '" + directory + "'.");
            }

            return manifest;
        }

        public static void Main(string[] args)
        {
            Arguments.Populate();

            if (Generate)
            {
                Console.WriteLine("Generating manifest" + (InputDirectory == string.Empty ? string.Empty : " for directory '" + InputDirectory) + "'...");

                if (InputDirectory != string.Empty && Directory.Exists(InputDirectory))
                {
                    PackageManifest manifest = PackageManifestFactory.GetDefault();

                    foreach (string file in Directory.EnumerateFiles(InputDirectory, "*", SearchOption.AllDirectories))
                    {
                    }
                }
                else
                {
                    Console.WriteLine("Unable to locate directory '" + InputDirectory + "'.  Exiting.");
                }
            }
        }
    }

    #endregion Private Methods
}