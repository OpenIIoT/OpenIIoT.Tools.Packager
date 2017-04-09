using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utility.CommandLine;
using System.IO;

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

        public static void Main(string[] args)
        {
            Console.WriteLine(Environment.CommandLine);

            Arguments.Populate();

            if (Generate != default(string))
            {
                Console.WriteLine("Generate: " + Generate);

                PackageManifest manifest = PackageManifestFactory.GetExamplePackageManifest();

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