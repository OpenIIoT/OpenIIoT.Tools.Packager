/*
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀  ▀  ▀      ▀▀
      █
      █   ███    █▄
      █   ███    ███
      █   ███    ███     ██     █   █        █      ██    ▄█   ▄
      █   ███    ███ ▀███████▄ ██  ██       ██  ▀███████▄ ██   █▄
      █   ███    ███     ██  ▀ ██▌ ██       ██▌     ██  ▀ ▀▀▀▀▀██
      █   ███    ███     ██    ██  ██       ██      ██    ▄█   ██
      █   ███    ███     ██    ██  ██▌    ▄ ██      ██    ██   ██
      █   ████████▀     ▄██▀   █   ████▄▄██ █      ▄██▀    █████
      █
 ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄▄  ▄▄ ▄▄   ▄▄▄▄ ▄▄     ▄▄     ▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄ ▄ ▄
 █████████████████████████████████████████████████████████████ ███████████████ ██  ██ ██   ████ ██     ██     ████████████████ █ █
      ▄
      █  Contains miscellaneous static methods.
      █
      █▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀ ▀▀▀▀▀▀▀▀▀▀▀ ▀ ▀▀▀     ▀▀               ▀
      █  The GNU Affero General Public License (GNU AGPL)
      █
      █  Copyright (C) 2017 JP Dillingham (jp@dillingham.ws)
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

using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OpenIIoT.Packager
{
    /// <summary>
    ///     Contains miscellaneous static methods.
    /// </summary>
    public static class Utility
    {
        #region Public Methods

        /// <summary>
        ///     Computes and returns the SHA512 hash of the specified file.
        /// </summary>
        /// <param name="file">the file for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified file.</returns>
        /// <exception cref="FileNotFoundException">Thrown the specified file can not be found.</exception>
        public static string GetFileSHA512Hash(string file)
        {
            if (File.Exists(file))
            {
                byte[] fileBytes = File.ReadAllBytes(file);
                byte[] hash;

                using (SHA512 sha512 = new SHA512Managed())
                {
                    hash = sha512.ComputeHash(fileBytes);
                }

                StringBuilder stringBuilder = new StringBuilder(128);

                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }

                return stringBuilder.ToString();
            }
            else
            {
                throw new FileNotFoundException($"The file {file} could not be found.");
            }
        }

        /// <summary>
        ///     Determines and returns the <see cref="PackageManifestFileType"/> matching the specified file.
        /// </summary>
        /// <param name="file">The file for which the <see cref="PackageManifestFileType"/> is to be determined.</param>
        /// <returns>The type of the specified file.</returns>
        public static PackageManifestFileType GetFileType(string file)
        {
            if (Path.GetExtension(file) == "dll")
            {
                return PackageManifestFileType.Binary;
            }
            else if (Path.GetFileName(file).Equals("index.html", StringComparison.OrdinalIgnoreCase) || Path.GetFileName(file).Equals("index.htm", StringComparison.OrdinalIgnoreCase))
            {
                return PackageManifestFileType.WebIndex;
            }
            else
            {
                return PackageManifestFileType.Resource;
            }
        }

        /// <summary>
        ///     Returns the path of the specified file, relative to the specified base directory.
        /// </summary>
        /// <param name="baseDirectory">The base directory of the relative file reference.</param>
        /// <param name="file">The file for which the relative path is to be returned.</param>
        /// <returns>The path of the specified file, relative to the specified base directory.</returns>
        public static string GetRelativePath(string baseDirectory, string file)
        {
            return file.Replace(baseDirectory, string.Empty);
        }

        /// <summary>
        ///     Serializes and returns as json the specified object.
        /// </summary>
        /// <param name="obj">The object to serialize.</param>
        /// <returns>The serialized object.</returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        #endregion Public Methods
    }
}