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
using OpenIIoT.SDK.Common;
using OpenIIoT.SDK.Package.Manifest;
using OpenIIoT.SDK.Package.Packaging;
using OpenIIoT.SDK.Package.Packaging.Operations;
using Utility.CommandLine;

namespace OpenIIoT.Tools.PackageUtility
{
    /// <summary>
    ///     The main Application class.
    /// </summary>
    internal class Program
    {
        #region Private Properties

        /// <summary>
        ///     Gets or sets the input directory for manifest and package generation.
        /// </summary>
        [Argument('d', "directory")]
        private static string Directory { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether files are hashed when generating a manifest.
        /// </summary>
        [Argument('h', "hash-files")]
        private static bool HashFiles { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether resource files are included when generating a manifest.
        /// </summary>
        [Argument('i', "include-resources")]
        private static bool IncludeResources { get; set; }

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
        ///     Gets or sets a value indicating whether an output file should be overwritten.
        /// </summary>
        [Argument('o', "overwrite")]
        private static bool Overwrite { get; set; }

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

        /// <summary>
        ///     Gets or sets a value indicating whether verification is to be skipped when extracting a pacakge file.
        /// </summary>
        [Argument('v', "skip-verification")]
        private static bool SkipVerification { get; set; }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        ///     The main entry point for the Application.
        /// </summary>
        /// <remarks>
        ///     The command line arguments are expected to start with an operand consisting of an application command, followed by
        ///     zero or more arguments and/or operands associated with the specified command. A complete list of commands and
        ///     arguments can be viewed in the <see cref="HelpPrinter"/> class, or via the command line by specifying the "help" command.
        /// </remarks>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            string command = string.Empty;

            try
            {
                Arguments.Populate();

                if (Operands.Count > 1)
                {
                    command = Operands[1].ToLower();
                }

                if (command == "manifest")
                {
                    ManifestGenerator generator = new ManifestGenerator();
                    generator.Updated += Update;

                    PackageManifest manifest = generator.GenerateManifest(Directory, IncludeResources, HashFiles, ManifestFile);

                    if (string.IsNullOrEmpty(ManifestFile) && manifest != default(PackageManifest))
                    {
                        Console.WriteLine(manifest.ToJson());
                    }
                }
                else if (command == "extract-manifest")
                {
                    ManifestExtractor extractor = new ManifestExtractor();
                    extractor.Updated += Update;

                    PackageManifest manifest = extractor.ExtractManifest(PackageFile, ManifestFile);

                    if (string.IsNullOrEmpty(ManifestFile) && manifest != default(PackageManifest))
                    {
                        Console.WriteLine(manifest.ToJson());
                    }
                }
                else if (command == "package")
                {
                    PackageCreator creator = new PackageCreator();
                    creator.Updated += Update;
                    creator.CreatePackage(Directory, ManifestFile, PackageFile, SignPackage, PrivateKeyFile, Passphrase, KeybaseUsername);
                }
                else if (command == "extract-package")
                {
                    PackageExtractor extractor = new PackageExtractor();
                    extractor.Updated += Update;
                    extractor.ExtractPackage(PackageFile, Directory, Overwrite, SkipVerification);
                }
                else if (command == "trust")
                {
                    PackageTruster truster = new PackageTruster();
                    truster.Updated += Update;
                    truster.TrustPackage(PackageFile, PrivateKeyFile, Passphrase);
                }
                else if (command == "verify")
                {
                    PackageVerifier verifier = new PackageVerifier();
                    verifier.Updated += Update;
                    verifier.VerifyPackage(PackageFile);
                }
                else
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

        /// <summary>
        ///     Event handler for events raised from the <see cref="SDK.Package.Packaging"/> namespace; writes the message and
        ///     operation to the console window.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="args">The event arguments.</param>
        private static void Update(object sender, PackagingUpdateEventArgs args)
        {
            string prefix = string.Empty;

            if (args.Type == PackagingUpdateType.Verbose)
            {
                prefix = new string(' ', 4);
            }
            else if (args.Type == PackagingUpdateType.Success)
            {
                prefix = "√ ";
            }

            Console.WriteLine($"[{args.Operation.ToString()}]: {prefix}{args.Message}");
        }
    }

    #endregion Private Methods
}