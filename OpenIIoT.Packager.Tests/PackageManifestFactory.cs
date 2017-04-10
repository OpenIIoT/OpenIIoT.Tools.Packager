using OpenIIoT.SDK.Package.Manifest;
using System;
using Xunit;

namespace OpenIIoT.Packager.Tests
{
    public class PackageManifestFactory
    {
        #region Public Methods

        [Fact]
        public void GetBlank()
        {
            PackageManifest test = Packager.PackageManifestFactory.GetBlank();

            Assert.NotNull(test);
            Assert.IsType<PackageManifest>(test);
        }

        [Fact]
        public void GetDefault()
        {
            PackageManifest test = Packager.PackageManifestFactory.GetDefault();

            Assert.NotNull(test);
            Assert.IsType<PackageManifest>(test);
            Assert.NotEqual(string.Empty, test.Title);
            Assert.NotEqual(string.Empty, test.Version);
        }

        #endregion Public Methods
    }
}