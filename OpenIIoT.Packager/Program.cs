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

        private static string GenerateManifest()
        {
            PackageManifest manifest = new PackageManifest();

            manifest.Title = "MyPlugin";
            manifest.Version = "1.0.0";
            manifest.Namespace = "MyCompany.MyApps";
            manifest.Description = "My plugin.";
            manifest.Publisher = "Me";
            manifest.Copyright = "Copyright (c) " + DateTime.Now.Year + " Me";
            manifest.License = "GNU GPLv3";
            manifest.Url = "http://github.com/";

            return JsonConvert.SerializeObject(manifest, new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        private static void Main(string[] args)
        {
            Arguments.Populate();

            if (Generate != default(string))
            {
                Console.WriteLine(GenerateManifest());
            }
        }

        #endregion Private Methods
    }
}