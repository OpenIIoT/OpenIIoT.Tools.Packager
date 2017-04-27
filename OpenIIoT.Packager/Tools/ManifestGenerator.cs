/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄▄▄▄███▄▄▄▄                                                                   ▄██████▄
      █    ▄██▀▀▀███▀▀▀██▄                                                                ███    ███
      █    ███   ███   ███   ▄█████  ██▄▄▄▄   █     ▄█████    ▄█████   ▄█████     ██      ███    █▀     ▄█████ ██▄▄▄▄     ▄█████    █████   ▄█████      ██     ██████     █████
      █    ███   ███   ███   ██   ██ ██▀▀▀█▄ ██    ██   ▀█   ██   █    ██  ▀  ▀███████▄  ▄███          ██   █  ██▀▀▀█▄   ██   █    ██  ██   ██   ██ ▀███████▄ ██    ██   ██  ██
      █    ███   ███   ███   ██   ██ ██   ██ ██▌  ▄██▄▄     ▄██▄▄      ██         ██  ▀ ▀▀███ ████▄   ▄██▄▄    ██   ██  ▄██▄▄     ▄██▄▄█▀   ██   ██     ██  ▀ ██    ██  ▄██▄▄█▀
      █    ███   ███   ███ ▀████████ ██   ██ ██  ▀▀██▀▀    ▀▀██▀▀    ▀███████     ██      ███    ███ ▀▀██▀▀    ██   ██ ▀▀██▀▀    ▀███████ ▀████████     ██    ██    ██ ▀███████
      █    ███   ███   ███   ██   ██ ██   ██ ██    ██        ██   █     ▄  ██     ██      ███    ███   ██   █  ██   ██   ██   █    ██  ██   ██   ██     ██    ██    ██   ██  ██
      █     ▀█   ███   █▀    ██   █▀  █   █  █     ██        ███████  ▄████▀     ▄██▀     ████████▀    ███████  █   █    ███████   ██  ██   ██   █▀    ▄██▀    ██████    ██  ██
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Generates and populates PackageManifest objects.
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
using System.IO;
using OpenIIoT.SDK.Package.Manifest;

namespace OpenIIoT.Packager.Tools
{
    /// <summary>
    ///     Generates and populates <see cref="PackageManifest"/> objects.
    /// </summary>
    public static class ManifestGenerator
    {
        #region Public Methods

        /// <summary>
        ///     Generates and populates <see cref="PackageManifest"/> objects from the specified directory, including resource
        ///     files and hashing file entries if those options are specified.
        /// </summary>
        /// <param name="inputDirectory">The directory from which to generate a list of files.</param>
        /// <param name="includeResources">A value indicating whether resource files are to be added to the manifest.</param>
        /// <param name="hashFiles">A value indicating whether files added to the manifest are to include a SHA512 hash.</param>
        /// <param name="manifestFile">The filename of the file to which the manifest is to be saved.</param>
        public static void GenerateManifest(string inputDirectory, bool includeResources, bool hashFiles, string manifestFile)
        {
            ValidateInputDirectoryArgument(inputDirectory);

            string[] files = Directory.GetFiles(inputDirectory, "*", SearchOption.AllDirectories);

            PackageManifestBuilder builder = new PackageManifestBuilder();

            Console.WriteLine("Generating new manifest...");

            builder.BuildDefault();

            Console.WriteLine($"Adding files from '{inputDirectory}'...");

            foreach (string file in files)
            {
                AddFile(builder, file, inputDirectory, includeResources, hashFiles);
            }

            PackageManifest manifest = builder.Manifest;

            Console.WriteLine("√ Manifest generated.");

            if (manifestFile != default(string))
            {
                try
                {
                    Console.WriteLine($"Saving output to file {manifestFile}...");
                    File.WriteAllText(manifestFile, manifest.ToJson());
                    Console.WriteLine("√ File saved successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unable to write to output file '{manifestFile}': {ex.Message}");
                }
            }
            else
            {
                Console.Write("\n" + manifest.ToJson());
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Adds the specified file from the specified directory to the specified builder, using the includeResources and
        ///     hashFiles parameters to determine whether the file should be added and hashed.
        /// </summary>
        /// <param name="builder">The manifest builder with which to add the file.</param>
        /// <param name="file">The file to add.</param>
        /// <param name="directory">The directory containing the file.</param>
        /// <param name="includeResources">A value indicating whether resource files are to be added to the manifest.</param>
        /// <param name="hashFiles">A value indicating whether files added to the manifest are to include a SHA512 hash.</param>
        private static void AddFile(PackageManifestBuilder builder, string file, string directory, bool includeResources, bool hashFiles)
        {
            PackageManifestFileType type = Utility.GetFileType(file);

            if (type == PackageManifestFileType.Binary || type == PackageManifestFileType.WebIndex || (type == PackageManifestFileType.Resource && includeResources))
            {
                Console.WriteLine($"Adding '{file}'...");
                PackageManifestFile newFile = new PackageManifestFile();

                newFile.Source = Utility.GetRelativePath(directory, file);

                if (type == PackageManifestFileType.Binary || hashFiles)
                {
                    newFile.Hash = "[deferred]";
                }

                builder.AddFile(type, newFile);
            }
            else
            {
                Console.WriteLine($"Skipping file '{file}...");
            }
        }

        /// <summary>
        ///     Validates the inputDirectory argument for <see cref="GenerateManifest(string, bool, bool, string)"/>.
        /// </summary>
        /// <param name="inputDirectory">The value specified for the inputDirectory argument.</param>
        /// <exception cref="ArgumentException">Thrown when the directory is a default or null string.</exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     Thrown when the directory can not be found on the local file system.
        /// </exception>
        /// <exception cref="FileNotFoundException">Thrown when the directory contains no files.</exception>
        private static void ValidateInputDirectoryArgument(string inputDirectory)
        {
            if (inputDirectory == default(string) || inputDirectory == string.Empty)
            {
                throw new ArgumentException($"The required argument 'directory' (-d) was not supplied.");
            }

            if (!Directory.Exists(inputDirectory))
            {
                throw new DirectoryNotFoundException($"The specified directory '{inputDirectory}' could not be found.");
            }

            if (Directory.GetFiles(inputDirectory, "*", SearchOption.AllDirectories).Length == 0)
            {
                throw new FileNotFoundException($"The specified directory '{inputDirectory}' is empty; Packages must contain at least one file.");
            }
        }

        #endregion Private Methods
    }
}