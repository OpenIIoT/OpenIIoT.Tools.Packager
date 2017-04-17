using OpenIIoT.SDK.Package.Manifest;
using System;
using System.IO;

namespace OpenIIoT.Packager
{
    public static class ManifestGenerator
    {
        #region Public Methods

        public static PackageManifest GenerateManifest(string directory = default(string), bool includeResources = default(bool), bool hashFiles = default(bool))
        {
            PackageManifestBuilder builder = new PackageManifestBuilder();

            Console.WriteLine("Generating new manifest...");

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

                            newFile.Source = Utility.GetRelativePath(directory, file);

                            if (type == PackageManifestFileType.Binary || hashFiles)
                            {
                                newFile.Hash = "[deferred]";
                            }

                            builder.AddFile(type, newFile);
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

            Console.WriteLine("Manifest generated.");

            return builder.Manifest;
        }

        #endregion Public Methods
    }
}