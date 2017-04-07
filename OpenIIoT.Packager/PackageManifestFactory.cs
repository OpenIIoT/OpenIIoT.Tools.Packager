using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenIIoT.Packager
{
    public class PackageManifestFactory
    {
        #region Public Methods

        public static PackageManifest GetExamplePackageManifest()
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

            return manifest;
        }

        #endregion Public Methods
    }
}