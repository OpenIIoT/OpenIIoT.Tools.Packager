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

            try
            {
                if (command == "manifest")
                {
                    Manifest();
                }
                else if (command == "package")
                {
                    PackageCreator.CreatePackage(InputDirectory, ManifestFile, PackageFile);
                }
                else if (command == "sign")
                {
                }
                else if (command == "verify")
                {
                }
                else if (command == "create-trust")
                {
                }
                else if (command == "verify-trust")
                {
                }
                else if (command == "help")
                {
                    HelpPrinter.PrintHelp(Operands.Count > 2 ? Operands[2] : default(string));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        ///     Generates a PackageManifest using the options passed to the application at run time.
        /// </summary>
        private static void Manifest()
        {
            if (InputDirectory != default(string) && !Directory.Exists(InputDirectory))
            {
                throw new DirectoryNotFoundException($"The specified directory '{InputDirectory}' could not be found.");
            }

            PackageManifest manifest = ManifestGenerator.GenerateManifest(InputDirectory, IncludeResources, HashFiles);

            if (ManifestFile != default(string))
            {
                try
                {
                    Console.WriteLine($"Saving output to file {ManifestFile}...");
                    File.WriteAllText(ManifestFile, manifest.ToJson());
                    Console.WriteLine("File saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to write to output file '{ManifestFile}': {ex.Message}");
                }
            }
            else
            {
                Console.Write("\n" + manifest.ToJson());
            }
        }

        /// <summary>
        ///     Creates a Package file using the options passed to the application at run time.
        /// </summary>
        private static void Package()
        {
            Console.WriteLine($"Creating package '{PackageFile}' from payload directory '{InputDirectory}' using manifest '{ManifestFile}'...");

            Console.WriteLine("Package created successfully.");
        }

        /// <summary>
        ///     Signs a Package file
        /// </summary>
        private static void Sign()
        {
        }
    }

    #endregion Private Methods
}