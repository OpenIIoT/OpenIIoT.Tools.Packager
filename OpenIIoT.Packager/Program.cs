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
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  The main Application class.
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
using OpenIIoT.Packager.Tools;
using OpenIIoT.SDK.Package.Manifest;
using Utility.CommandLine;
using Newtonsoft.Json.Linq;

namespace OpenIIoT.Packager
{
    /// <summary>
    ///     The main Application class.
    /// </summary>
    internal class Program
    {
        #region Private Properties

        /// <summary>
        ///     Gets or sets a value indicating whether files are hashed when generating a manifest.
        /// </summary>
        [Argument('h', "hash-files")]
        private static bool HashFiles { get; set; }

        /// <summary>
        ///     Gets or sets a help topic; used in lieu of the "help" command.
        /// </summary>
        [Argument('?', "help")]
        private static string Help { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether resource files are included when generating a manifest.
        /// </summary>
        [Argument('i', "include-resources")]
        private static bool IncludeResources { get; set; }

        /// <summary>
        ///     Gets or sets the input directory for manifest and package generation.
        /// </summary>
        [Argument('d', "directory")]
        private static string InputDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the Keybase.io username of the account hosting the PGP public key used for digest verification.
        /// </summary>
        [Argument('u', "keybase-username")]
        private static string KeybaseUsername { get; set; }

        /// <summary>
        ///     Gets or sets the input manifest for package generation.
        /// </summary>
        [Argument('m', "manifest")]
        private static string ManifestFile { get; set; }

        /// <summary>
        ///     Gets or sets the list of command line operands.
        /// </summary>
        [Operands]
        private static List<string> Operands { get; set; }

        /// <summary>
        ///     Gets or sets the package for package generation, signing, and verification.
        /// </summary>
        [Argument('p', "package")]
        private static string PackageFile { get; set; }

        /// <summary>
        ///     Gets or sets the passphrase for the private key.
        /// </summary>
        [Argument('a', "passphrase")]
        private static string Passphrase { get; set; }

        /// <summary>
        ///     Gets or sets the filename of the file containing the ASCII-armored PGP private key.
        /// </summary>
        [Argument('r', "private-key")]
        private static string PrivateKeyFile { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the package file should be signed during a package operation.
        /// </summary>
        [Argument('s', "sign")]
        private static bool SignPackage { get; set; }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        ///     The main entry point for the Application.
        /// </summary>
        /// <remarks>
        ///     The command line arguments are expected to start with an operand consisting of an application command, followed by
        ///     zero or more arguments and/or operands associated with the specified command. A complete list of commands and
        ///     arguments can be viewed in the <see cref="Tools.HelpPrinter"/> class, or via the command line by specifying the
        ///     "help" command.
        /// </remarks>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            try
            {
                Arguments.Populate();

                string command = "help";

                if (Operands.Count > 1)
                {
                    command = Operands[1].ToLower();
                }

                if (Help != default(string))
                {
                    HelpPrinter.PrintHelp(Help);
                    return;
                }

                if (command == "manifest")
                {
                    ManifestGenerator.GenerateManifest(InputDirectory, IncludeResources, HashFiles, ManifestFile);
                }
                else if (command == "package")
                {
                    PackageCreator.CreatePackage(InputDirectory, ManifestFile, PackageFile, SignPackage, PrivateKeyFile, Passphrase, KeybaseUsername);
                }
                else if (command == "trust")
                {
                }
                else if (command == "verify")
                {
                    Console.WriteLine(Utility.FetchPublicKeyForUser("max"));
                    Console.WriteLine(Utility.FetchPublicKeyForUser("max"));
                }
                else if (command == "help")
                {
                    HelpPrinter.PrintHelp(Operands.Count > 2 ? Operands[2] : default(string));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }

    #endregion Private Methods
}