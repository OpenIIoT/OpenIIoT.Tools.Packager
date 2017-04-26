/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █      ▄███████▄                                                              ▄████████
      █     ███    ███                                                              ███    ███
      █     ███    ███   ▄█████   ▄██████    █  █▄     ▄█████     ▄████▄     ▄█████ ███    █▀     █████    ▄█████   ▄█████      ██     ██████     █████
      █     ███    ███   ██   ██ ██    ██   ██ ▄██▀    ██   ██   ██    ▀    ██   █  ███          ██  ██   ██   █    ██   ██ ▀███████▄ ██    ██   ██  ██
      █   ▀█████████▀    ██   ██ ██    ▀    ██▐█▀      ██   ██  ▄██        ▄██▄▄    ███         ▄██▄▄█▀  ▄██▄▄      ██   ██     ██  ▀ ██    ██  ▄██▄▄█▀
      █     ███        ▀████████ ██    ▄  ▀▀████     ▀████████ ▀▀██ ███▄  ▀▀██▀▀    ███    █▄  ▀███████ ▀▀██▀▀    ▀████████     ██    ██    ██ ▀███████
      █     ███          ██   ██ ██    ██   ██ ▀██▄    ██   ██   ██    ██   ██   █  ███    ███   ██  ██   ██   █    ██   ██     ██    ██    ██   ██  ██
      █    ▄████▀        ██   █▀ ██████▀    ▀█   ▀█▀   ██   █▀   ██████▀    ███████ ████████▀    ██  ██   ███████   ██   █▀    ▄██▀    ██████    ██  ██
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Creates Package files.
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
using System.IO.Compression;
using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;

namespace OpenIIoT.Packager.Tools
{
    /// <summary>
    ///     Creates Package files.
    /// </summary>
    public static class PackageCreator
    {
        #region Public Methods

        /// <summary>
        ///     Creates a Package file with the specified filename from the specified input directory using the specified manifest.
        /// </summary>
        /// <param name="inputDirectory">The directory containing the Package contents.</param>
        /// <param name="manifestFile">The PackageManifest file for the Package.</param>
        /// <param name="packageFile">The filename to which the Package file will be saved.</param>
        public static void CreatePackage(string inputDirectory, string manifestFile, string packageFile)
        {
            // validate input
            if (inputDirectory == default(string))
            {
                throw new ArgumentException($"The required argument 'directory' (-d|--directory) was not supplied.");
            }

            if (!Directory.Exists(inputDirectory))
            {
                throw new DirectoryNotFoundException($"The specified directory '{inputDirectory}' could not be found.");
            }

            if (Directory.GetFiles(inputDirectory, "*", SearchOption.AllDirectories).Length == 0)
            {
                throw new ArgumentException($"The specified directory '{inputDirectory}' is empty; Packages must contain at least one file.");
            }

            if (manifestFile == default(string))
            {
                throw new ArgumentException("The required argument 'manifest' (-m|--manifest) was not supplied.");
            }

            if (!File.Exists(manifestFile))
            {
                throw new FileNotFoundException($"The specified file '{manifestFile}' could not be found.");
            }

            // fetch and deserialize the PackageManifest from the specified file
            PackageManifest manifest;

            try
            {
                manifest = OpenPackageManifest(manifestFile);
            }
            catch (Exception ex)
            {
                throw new Exception($"The specified manifest file '{manifestFile}' could not be opened: {ex.Message}");
            }

            if (packageFile == default(string))
            {
                throw new ArgumentException($"The required argument 'package' (-p|--package) was not supplied.");
            }

            try
            {
                File.WriteAllText(packageFile, "Hello World!");
                File.Delete(packageFile);
            }
            catch (Exception ex)
            {
                throw new Exception($"The specified package file '{packageFile}' could not be written: {ex.Message}");
            }

            // input validated. begin package creation.
            string tempDirectory = Path.Combine(Path.GetTempPath(), System.Reflection.Assembly.GetEntryAssembly().GetName().Name, Guid.NewGuid().ToString());
            string payloadDirectory = Path.Combine(tempDirectory, SDK.Package.Constants.PayloadDirectoryName);
            string payloadArchiveName = Path.Combine(tempDirectory, SDK.Package.Constants.PayloadArchiveName);
            string tempPackageFileName = Path.Combine(tempDirectory, Path.GetFileName(packageFile));

            try
            {
                Console.WriteLine($"Creating temporary directory '{tempDirectory}'...");
                Directory.CreateDirectory(tempDirectory);
                Console.WriteLine($"√ Directory created.");

                Console.WriteLine($"Copying input directory '{inputDirectory}' to '{payloadDirectory}'...");
                Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(inputDirectory, payloadDirectory);
                Console.WriteLine($"√ Directory copied successfully.");

                Console.WriteLine($"Validating manifest '{manifestFile}' and generating SHA512 hashes...");
                ValidateManifestAndGenerateHashes(manifest, payloadDirectory);
                Console.WriteLine($"√ Manifest validated and hashes written.");

                Console.WriteLine($"Compressing payload into '{payloadArchiveName}'...");
                ZipFile.CreateFromDirectory(payloadDirectory, payloadArchiveName);
                Console.WriteLine($"√ Successfully compressed payload.");

                Console.WriteLine($"Deleting temporary payload directory '{payloadDirectory}...");
                Directory.Delete(payloadDirectory, true);
                Console.WriteLine($"√ Successfully deleted temporary payload directory.");

                Console.WriteLine($"Updating manifest with SHA512 hash of payload archive...");
                manifest.Hash = Utility.GetFileSHA512Hash(payloadArchiveName);
                Console.WriteLine($"√ Hash computed successfully: {manifest.Hash}.");

                Console.WriteLine($"Writing manifest to 'manifest.json' in '{tempDirectory}'...");
                WriteManifest(manifest, tempDirectory);
                Console.WriteLine($"√ Manifest written successfully.");

                Console.WriteLine($"Packaging manifest and payload into '{packageFile}'...");
                ZipFile.CreateFromDirectory(tempDirectory, packageFile);
                Console.WriteLine($"√ Package created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine($"Deleting temporary files...");
                Directory.Delete(tempDirectory, true);
                Console.WriteLine($"√ Temporary files deleted successfully.");
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        ///     Reads the contents of the specified file and attempts to deserialize it to an instance of
        ///     <see cref="PackageManifest"/> .
        /// </summary>
        /// <param name="manifestFile">The file from which the manifest is read.</param>
        /// <returns>The deserialized content of the manifest file.</returns>
        private static PackageManifest OpenPackageManifest(string manifestFile)
        {
            string manifestContents = File.ReadAllText(manifestFile);

            return JsonConvert.DeserializeObject<PackageManifest>(manifestContents);
        }

        /// <summary>
        ///     Compares the specified manifest to the contents of the specified directory and hashes any files that had been
        ///     deferred for hashing in the manifest.
        /// </summary>
        /// <param name="manifest">The manifest for which validation and hash generation is to be performed.</param>
        /// <param name="directory">The directory containing payload files.</param>
        private static void ValidateManifestAndGenerateHashes(PackageManifest manifest, string directory)
        {
            foreach (PackageManifestFileType type in manifest.Files.Keys)
            {
                foreach (PackageManifestFile file in manifest.Files[type])
                {
                    // determine the absolute path for the file we need to examine
                    string fileToCheck = Path.Combine(directory, file.Source);

                    if (!File.Exists(fileToCheck))
                    {
                        throw new FileNotFoundException($"The file '{file.Source}' is listed in the manifest but is not found on disk.");
                    }

                    if (file.Hash != string.Empty)
                    {
                        file.Hash = Utility.GetFileSHA512Hash(fileToCheck);
                    }
                }
            }
        }

        /// <summary>
        ///     Serializes the specified manifest to JSON and writes it to a 'manifest.json' file in the specified directory.
        /// </summary>
        /// <param name="manifest">The manifest to serialize and write.</param>
        /// <param name="directory">The directory into which the generated file will be written.</param>
        private static void WriteManifest(PackageManifest manifest, string directory)
        {
            string destinationFile = Path.Combine(directory, "manifest.json");
            string contents = manifest.ToJson();

            File.WriteAllText(destinationFile, contents);
        }

        #endregion Private Methods
    }
}