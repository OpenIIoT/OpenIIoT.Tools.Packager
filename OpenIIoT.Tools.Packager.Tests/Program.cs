/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄███████▄
      █     ███    ███
      █     ███    ███    █████  ██████     ▄████▄     █████   ▄█████     ▄▄██▄▄▄
      █     ███    ███   ██  ██ ██    ██   ██    ▀    ██  ██   ██   ██  ▄█▀▀██▀▀█▄
      █   ▀█████████▀   ▄██▄▄█▀ ██    ██  ▄██        ▄██▄▄█▀   ██   ██  ██  ██  ██
      █     ███        ▀███████ ██    ██ ▀▀██ ███▄  ▀███████ ▀████████  ██  ██  ██
      █     ███          ██  ██ ██    ██   ██    ██   ██  ██   ██   ██  ██  ██  ██
      █    ▄████▀        ██  ██  ██████    ██████▀    ██  ██   ██   █▀   █  ██  █
      █
      █       ███
      █   ▀█████████▄
      █      ▀███▀▀██    ▄█████   ▄█████     ██      ▄█████
      █       ███   ▀   ██   █    ██  ▀  ▀███████▄   ██  ▀
      █       ███      ▄██▄▄      ██         ██  ▀   ██
      █       ███     ▀▀██▀▀    ▀███████     ██    ▀███████
      █       ███       ██   █     ▄  ██     ██       ▄  ██
      █      ▄████▀     ███████  ▄████▀     ▄██▀    ▄████▀
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Unit tests for the Program class.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2016-2017 JP Dillingham (jp@dillingham.ws)
      █
      █  This program is free software: you can redistribute it and/or modify
      █  it under the terms of the GNU Affero General Public License as published by
      █  the Free Software Foundation, either version 3 of the License, or
      █  (at your option) any later version.
      █
      █  This program is distributed in the hope that it will be useful,
      █  but WITHOUT ANY WARRANTY; without even the implied warranty of
      █  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
      █  GNU Affero General Public License for more details.
      █
      █  You should have received a copy of the GNU Affero General Public License
      █  along with this program.  If not, see <http://www.gnu.org/licenses/>.
      █
      ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀  ▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀██
                                                                                                   ██
                                                                                               ▀█▄ ██ ▄█▀
                                                                                                 ▀████▀
                                                                                                   ▀▀                            */

using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace OpenIIoT.Tools.Packager.Tests
{
    /// <summary>
    ///     Unit tests for the HelpPrinter class.
    /// </summary>
    public class Program
    {
        #region Public Constructors

        public Program()
        {
            Uri codeBaseUri = new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            string codeBasePath = Uri.UnescapeDataString(codeBaseUri.AbsolutePath);
            string dirPath = Path.GetDirectoryName(codeBasePath);

            DataDirectory = Path.Combine(dirPath, "Data");

            TempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(TempDirectory);
        }

        #endregion Public Constructors

        #region Private Properties

        private string DataDirectory { get; set; }

        private string TempDirectory { get; set; }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        ///     Disposes this instance of <see cref="Packager"/>.
        /// </summary>
        public void Dispose()
        {
            Directory.Delete(TempDirectory, true);
        }

        [Fact]
        public void ProcessExtractManifest()
        {
            string package = Path.Combine(DataDirectory, "package.zip");

            Tools.Packager.Program.Process($"opkg.exe extract-manifest -p {package}");
        }

        [Fact]
        public void ProcessExtractManifestFile()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string manifest = Path.Combine(TempDirectory, "extractedmanifest.json");

            Tools.Packager.Program.Process($"opkg.exe extract-manifest -m {manifest} -p {package}");
        }

        [Fact]
        public void ProcessExtractPackage()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string directory = Path.Combine(TempDirectory, "payload");

            Tools.Packager.Program.Process($"opkg.exe extract-package -p {package} -d {directory}");
        }

        [Fact]
        public void ProcessExtractPackageBad()
        {
            Tools.Packager.Program.Process($"opkg.exe extract-package");
        }

        [Fact]
        public void ProcessHelp()
        {
            Tools.Packager.Program.Process("opkg.exe help");
        }

        [Fact]
        public void ProcessManifest()
        {
            string directory = Path.Combine(DataDirectory, "Files");

            Tools.Packager.Program.Process($"opkg.exe manifest -d {directory}");
        }

        [Fact]
        public void ProcessManifestBad()
        {
            Tools.Packager.Program.Process($"opkg.exe manifest");
        }

        [Fact]
        public void ProcessManifestFile()
        {
            string directory = Path.Combine(DataDirectory, "Files");
            string manifest = Path.Combine(TempDirectory, "newmanifest.json");

            Tools.Packager.Program.Process($"opkg.exe manifest -m {manifest} -d {directory}");
        }

        [Fact]
        public void ProcessPackage()
        {
            string directory = Path.Combine(DataDirectory, "Files");
            string manifest = Path.Combine(DataDirectory, "manifest.json");
            string package = Path.Combine(TempDirectory, "createdpackage.zip");

            Tools.Packager.Program.Process($"opkg.exe package -d {directory} -m {manifest} -p {package}");
        }

        [Fact]
        public void ProcessPackageBad()
        {
            Tools.Packager.Program.Process($"opkg.exe package");
        }

        [Fact]
        public void ProcessTrust()
        {
            string package = Path.Combine(TempDirectory, "signedpackage.zip");
            File.Copy(Path.Combine(DataDirectory, "signedpackage.zip"), package);

            string key = Path.Combine(DataDirectory, "private.asc");
            string passphrase = File.ReadAllText(Path.Combine(DataDirectory, "passphrase.txt"));

            Tools.Packager.Program.Process($"opkg.exe trust -p {package} -r {key} -a {passphrase}");
        }

        [Fact]
        public void ProcessTrustBad()
        {
            Tools.Packager.Program.Process($"opkg.exe trust");
        }

        [Fact]
        public void ProcessVerify()
        {
            string package = Path.Combine(DataDirectory, "package.zip");

            Tools.Packager.Program.Process($"opkg.exe verify -p {package}");
        }

        [Fact]
        public void ProcessVerifyBad()
        {
            Tools.Packager.Program.Process($"opkg.exe verify");
        }

        #endregion Public Methods
    }
}