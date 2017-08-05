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

        /// <summary>
        ///     Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
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

        /// <summary>
        ///     Gets or sets the directory containing test data
        /// </summary>
        private string DataDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the temporary directory for tests data
        /// </summary>
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

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with no arguments.
        /// </summary>
        [Fact]
        public void Process()
        {
            Tools.Packager.Program.Process();
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the extract-manifest command.
        /// </summary>
        [Fact]
        public void ProcessExtractManifest()
        {
            string package = Path.Combine(DataDirectory, "package.zip");

            Tools.Packager.Program.Process($"opkg.exe extract-manifest -p {package}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the extract-manifest command with an
        ///     output file.
        /// </summary>
        [Fact]
        public void ProcessExtractManifestFile()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string manifest = Path.Combine(TempDirectory, "extractedmanifest.json");

            Tools.Packager.Program.Process($"opkg.exe extract-manifest -m {manifest} -p {package}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the extract-package command and package
        ///     and directory arguments.
        /// </summary>
        [Fact]
        public void ProcessExtractPackage()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string directory = Path.Combine(TempDirectory, "payload");

            Tools.Packager.Program.Process($"opkg.exe extract-package -p {package} -d {directory}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the extract-package command.
        /// </summary>
        [Fact]
        public void ProcessExtractPackageBad()
        {
            Tools.Packager.Program.Process("opkg.exe extract-package");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the extract-package command and package,
        ///     directory, and public key arguments.
        /// </summary>
        [Fact]
        public void ProcessExtractPackageKey()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string directory = Path.Combine(TempDirectory, "payload");
            string keyFile = Path.Combine(DataDirectory, "public.asc");

            Tools.Packager.Program.Process($"opkg.exe extract-package -p {package} -d {directory} -b {keyFile}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the help command.
        /// </summary>
        [Fact]
        public void ProcessHelp()
        {
            Tools.Packager.Program.Process("opkg.exe help");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the help command and a topic.
        /// </summary>
        [Fact]
        public void ProcessHelpTopic()
        {
            Tools.Packager.Program.Process("opkg.exe help manifest");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the manifest command and directory argument.
        /// </summary>
        [Fact]
        public void ProcessManifest()
        {
            string directory = Path.Combine(DataDirectory, "Files");

            Tools.Packager.Program.Process($"opkg.exe manifest -d {directory}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the manifest command.
        /// </summary>
        [Fact]
        public void ProcessManifestBad()
        {
            Tools.Packager.Program.Process("opkg.exe manifest");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the manifest command and manifest and
        ///     directory arguments.
        /// </summary>
        [Fact]
        public void ProcessManifestFile()
        {
            string directory = Path.Combine(DataDirectory, "Files");
            string manifest = Path.Combine(TempDirectory, "newmanifest.json");

            Tools.Packager.Program.Process($"opkg.exe manifest -m {manifest} -d {directory}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with one argument.
        /// </summary>
        [Fact]
        public void ProcessOneArgument()
        {
            Tools.Packager.Program.Process("okpg.exe");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the package command and directory,
        ///     manifest and package arguments.
        /// </summary>
        [Fact]
        public void ProcessPackage()
        {
            string directory = Path.Combine(DataDirectory, "Files");
            string manifest = Path.Combine(DataDirectory, "manifest.json");
            string package = Path.Combine(TempDirectory, "createdpackage.zip");

            Tools.Packager.Program.Process($"opkg.exe package -d {directory} -m {manifest} -p {package}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the package command.
        /// </summary>
        [Fact]
        public void ProcessPackageBad()
        {
            Tools.Packager.Program.Process("opkg.exe package");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the package command and directory,
        ///     manifest, package, and the necessary arguments to create a signed package.
        /// </summary>
        [Fact]
        public void ProcessPackageSigned()
        {
            string directory = Path.Combine(DataDirectory, "Files");
            string manifest = Path.Combine(DataDirectory, "manifest.json");
            string package = Path.Combine(TempDirectory, "createdpackage.zip");
            string keyFile = Path.Combine(DataDirectory, "private.asc");
            string passphrase = File.ReadAllText(Path.Combine(DataDirectory, "passphrase.txt"));
            string username = "openiiottest";

            Tools.Packager.Program.Process($"opkg.exe package -d {directory} -m {manifest} -p {package} -s -r {keyFile} -a {passphrase} -u {username}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the trust command and package, key and
        ///     passphrase arguments.
        /// </summary>
        [Fact]
        public void ProcessTrust()
        {
            string package = Path.Combine(TempDirectory, "signedpackage.zip");
            File.Copy(Path.Combine(DataDirectory, "signedpackage.zip"), package);

            string key = Path.Combine(DataDirectory, "private.asc");
            string passphrase = File.ReadAllText(Path.Combine(DataDirectory, "passphrase.txt"));

            Tools.Packager.Program.Process($"opkg.exe trust -p {package} -r {key} -a {passphrase}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the trust command.
        /// </summary>
        [Fact]
        public void ProcessTrustBad()
        {
            Tools.Packager.Program.Process("opkg.exe trust");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the verify command and package argument.
        /// </summary>
        [Fact]
        public void ProcessVerify()
        {
            string package = Path.Combine(DataDirectory, "package.zip");

            Tools.Packager.Program.Process($"opkg.exe verify -p {package}");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the verify command.
        /// </summary>
        [Fact]
        public void ProcessVerifyBad()
        {
            Tools.Packager.Program.Process("opkg.exe verify");
        }

        /// <summary>
        ///     Tests the <see cref="Tools.Packager.Program.Process(string)"/> method with the verify command and package and
        ///     public key arguments.
        /// </summary>
        [Fact]
        public void ProcessVerifyExplicitKey()
        {
            string package = Path.Combine(DataDirectory, "package.zip");
            string keyFile = Path.Combine(DataDirectory, "public.asc");

            Tools.Packager.Program.Process($"opkg.exe verify -p {package} -b {keyFile}");
        }

        #endregion Public Methods
    }
}