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
        /// <param name="directory">The directory from which to generate a list of files.</param>
        /// <param name="includeResources">A value indicating whether resource files are to be added to the manifest.</param>
        /// <param name="hashFiles">A value indicating whether files added to the manifest are to include a SHA512 hash.</param>
        /// <returns>The generated and populated manifest.</returns>
        public static PackageManifest GenerateManifest(string directory = default(string), bool includeResources = false, bool hashFiles = false)
        {
            PackageManifestBuilder builder = new PackageManifestBuilder();

            Console.WriteLine("Generating new manifest...");

            builder.BuildDefault();

            if (directory != default(string) && directory != string.Empty)
            {
                if (Directory.Exists(directory))
                {
                    Console.WriteLine($"Adding files from '{directory}'...");

                    foreach (string file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
                    {
                        AddFile(builder, file, directory, includeResources, hashFiles);
                    }
                }
                else
                {
                    Console.WriteLine($"Couldn't find input directory '{directory}'.");
                }
            }

            Console.WriteLine("Manifest generated.");

            return builder.Manifest;
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

        #endregion Private Methods
    }
}