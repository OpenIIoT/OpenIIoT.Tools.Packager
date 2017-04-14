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

        [Argument('h', "hash-files")]
        private static bool a { get; set; }

        [Argument('g', "generate-manifest")]
        private static string Generate { get; set; }

        [Argument('m', "manifest")]
        private static string Manifest { get; set; }

        [Operands]
        private static string[] Operands { get; set; }

        [Argument('p', "payload")]
        private static string Payload { get; set; }

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

                        manifest.Files.Add(new PackageManifestFile() { Source = Utility.MakeRelativePath(directory, file), Hash = res });
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
            Console.WriteLine(Environment.CommandLine);

            Arguments.Populate();

            if (Generate != default(string))
            {
                Console.WriteLine("Generate: " + Generate);

                PackageManifest manifest = GenerateManifest(Operands[1]);

                if (Generate == string.Empty)
                {
                    Console.WriteLine(manifest.ToJson());
                }
                else
                {
                    try
                    {
                        File.WriteAllText(Generate, manifest.ToJson());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error writing to output file '" + Generate + "': " + ex.Message);
                    }
                }
            }
        }

        #endregion Private Methods
    }
}