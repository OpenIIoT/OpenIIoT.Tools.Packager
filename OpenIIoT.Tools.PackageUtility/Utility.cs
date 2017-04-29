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
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;
using Newtonsoft.Json.Linq;

namespace OpenIIoT.Tools.PackageUtility
{
    /// <summary>
    ///     Contains miscellaneous static methods.
    /// </summary>
    public static class Utility
    {
        #region Public Methods

        /// <summary>
        ///     Fetches an object containing key information for the specified username.
        /// </summary>
        /// <param name="username">The username for which the associated key information is to be fetched.</param>
        /// <returns>The fetched object.</returns>
        public static JObject FetchKeyInfo(string username)
        {
            return FetchObjectFromURL(GetKeyInfoUrlForUser(username));
        }

        /// <summary>
        ///     Fetches and deserializes an object from the specified URL.
        /// </summary>
        /// <param name="url">The fully qualified URL from which the object is to be fetched.</param>
        /// <returns>The deserialized fetched object.</returns>
        /// <exception cref="WebException">Thrown when an error occurs fetching the object.</exception>
        public static JObject FetchObjectFromURL(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString(url);
                    return JObject.Parse(content);
                }
            }
            catch (Exception ex)
            {
                throw new WebException($"Failed to fetch the object from '{url}': {ex.Message}");
            }
        }

        /// <summary>
        ///     Fetches the primary PGP public key for the specified keybase.io user.
        /// </summary>
        /// <param name="username">The username of the keybase.io user for which to fetch the key.</param>
        /// <returns>The retrieved PGP public key.</returns>
        public static string FetchPublicKeyForUser(string username)
        {
            JObject key = FetchKeyInfo(username);
            string publicKey = key["them"]["public_keys"]["primary"]["bundle"].ToString();

            if (publicKey.Length < Constants.KeyMinimumLength)
            {
                throw new InvalidDataException($"The length of the retrieved key was not long enough (expected: >= {Constants.KeyMinimumLength}, actual: {publicKey.Length}) to be a valid PGP public key.");
            }

            return publicKey;
        }

        /// <summary>
        ///     Computes and returns the SHA512 hash of the specified file.
        /// </summary>
        /// <param name="file">The file for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified file.</returns>
        /// <exception cref="FileNotFoundException">Thrown when the specified file can not be found.</exception>
        public static string GetFileSHA512Hash(string file)
        {
            if (File.Exists(file))
            {
                return GetStringSHA512Hash(File.ReadAllText(file));
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
        ///     Returns the url used to retrieve key information for the specified user.
        /// </summary>
        /// <param name="username">The user for which key information is to be returned.</param>
        /// <returns>The key information url.</returns>
        public static string GetKeyInfoUrlForUser(string username)
        {
            return Constants.KeyUrlBase.Replace(Constants.KeyUrlPlaceholder, username);
        }

        /// <summary>
        ///     Returns the path of the specified file, relative to the specified base directory.
        /// </summary>
        /// <param name="baseDirectory">The base directory of the relative file reference.</param>
        /// <param name="file">The file for which the relative path is to be returned.</param>
        /// <returns>The path of the specified file, relative to the specified base directory.</returns>
        public static string GetRelativePath(string baseDirectory, string file)
        {
            if (!baseDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                baseDirectory += Path.DirectorySeparatorChar;
            }

            return file.Replace(baseDirectory, string.Empty);
        }

        /// <summary>
        ///     Computes and returns the SHA512 hash of the specified string.
        /// </summary>
        /// <param name="content">The string for which the SHA512 hash is to be computed.</param>
        /// <returns>The SHA512 hash of the specified string.</returns>
        public static string GetStringSHA512Hash(string content)
        {
            byte[] contentBytes = Encoding.ASCII.GetBytes(content);
            byte[] hash;

            using (SHA512 sha512 = new SHA512Managed())
            {
                hash = sha512.ComputeHash(contentBytes);
            }

            StringBuilder stringBuilder = new StringBuilder(128);

            foreach (byte b in hash)
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString();
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