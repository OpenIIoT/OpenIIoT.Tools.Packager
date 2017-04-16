using OpenIIoT.SDK.Package.Manifest;
using System;
using Utility.CommandLine;
using System.IO;

namespace OpenIIoT.Packager
{
    internal class Program
    {
        #region Private Properties

        [Argument('g', "generate-manifest")]
        private static bool GenerateManifest { get; set; }

        [Argument('h', "hash")]
        private static bool Hash { get; set; }

        [Argument('?', "help")]
        private static bool Help { get; set; }

        [Argument('i', "include-resources")]
        private static bool IncludeResources { get; set; }

        [Argument('d', "directory")]
        private static string InputDirectory { get; set; }

        [Argument('m', "manifest")]
        private static string Manifest { get; set; }

        [Argument('o', "output")]
        private static string OutputFile { get; set; }

        [Argument('p', "create-package")]
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
                if (Package)
                {
                    Console.WriteLine("Can't generate a manifest and create a package at the same time; generate first, then package.");
                }

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
            else if (Package)
            {
                // TODO : create package etc
            }
            else
            {
                Console.WriteLine("Select an operation; either generate a manifest (-g or --generate-manifest) or create a package (-p or --create-package).");
                Console.WriteLine("Start the application with -h or --help for a full explanation of arguments.");
            }
        }
    }

    #endregion Private Methods
}