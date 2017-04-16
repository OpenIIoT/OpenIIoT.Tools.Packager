using OpenIIoT.SDK.Package.Manifest;
using System;
using System.IO;

namespace OpenIIoT.Packager
{
    public static class ManifestGenerator
    {
        public static PackageManifest GenerateManifest(string directory = default(string), bool includeResources = default(bool), bool hash = default(bool))
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
                                newFile.Hash = "[Deferred until packaging]";
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
    }
}