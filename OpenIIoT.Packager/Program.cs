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

        [Argument('g', "generate-manifest")]
        private static bool GenerateManifest { get; set; }

        [Argument('o', "output")]
        private static string OutputFile { get; set; }

        [Operands]
        private static string[] Operands { get; set; }

        [Argument('p', "package")]
        private static bool Package { get; set; }

        #endregion Private Properties

        #region Private Methods

        public static PackageManifest GenerateDefaultManifest(string directory = default(string), bool includeResources = default(bool), bool hash = default(bool))
        {
            PackageManifestBuilder builder = new PackageManifestBuilder();
            builder.BuildDefault();

            if (directory != default(string) && directory != string.Empty)
            {
                if (Directory.Exists(directory))
                {
                    Console.WriteLine($"Adding files from '{directory}'...");

                    foreach (string file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
                    {
                        PackageManifestFileType type = Utility.GetFileType(file);

                        if (type == PackageManifestFileType.Binary || type == PackageManifestFileType.WebIndex || (type == PackageManifestFileType.Resource && includeResources))
                        {
                            Console.WriteLine($"Adding '{file}'...");
                            PackageManifestFile newFile = new PackageManifestFile();

                            newFile.Type = type;
                            newFile.Source = Utility.GetRelativePath(directory, file);

                            if (hash)
                            {
                                newFile.Hash = Utility.GetFileSHA512Hash(file);
                            }

                            builder.AddFile(newFile);
                        }
                        else
                        {
                            Console.WriteLine($"Skipping file '{file}...");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Couldn't find input directory '{directory}'.");
                }
            }

            return builder.Manifest;
        }

        public static void Main(string[] args)
        {
            Arguments.Populate();

            if (GenerateManifest)
            {
                PackageManifest manifest = GenerateDefaultManifest(InputDirectory, IncludeResources, Hash);

                if (OutputFile != default(string))
                {
                    try
                    {
                        File.WriteAllText(OutputFile, manifest.ToJson());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unable to write to output file '{OutputFile}'");
                    }
                }
                else
                {
                    Console.Write(manifest.ToJson());
                }
            }
        }
    }

    #endregion Private Methods
}